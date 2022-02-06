using Afca.Common;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using FrameworkForge.UI;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afca.UI
{
    public partial class AgmEnvioSecundarioDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        Task uiTask;
        UltraGridRow currentSendingRow;
        string CallerEntidadId;

        public AgmEnvioSecundarioDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            SetCallerEntidadId();
            SetGridColumnsAppearance();
            LoadData();
        }
        protected override void InitializeAppearance()
        {
            base.InitializeAppearance();
            ControlsThemeBuilder.GridMakeUp(this.ultraGridData, ThemeColor.Gris);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0], ThemeColor.Gris);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[1], ThemeColor.Verde);

            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbCheckSelected", "IconoSelectAll");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbUncheckSelected", "IconoUnselectAll");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbSend", "IconoPlay");

            foreach (string tbbkey in new string[] { "tbbAddNew", "tbbDelete", "tbbSave" })
                ultraToolbarsManager.Tools[tbbkey].SharedProps.Enabled = false;



            foreach (UltraGridBand bi in ultraGridData.DisplayLayout.Bands)
            {
                if (!bi.CardView)
                {
                    foreach (UltraGridColumn col in bi.Columns)
                        col.PerformAutoResize();
                }
                else
                    bi.PerformAutoResizeCards(false);
            }
        }
        void SetCallerEntidadId() {
            switch (itemContainer.Name)
            {
                case "AgmEnviosRecibos":
                   CallerEntidadId = "Recibos";
                    break;
                case "AgmEnviosRCS":
                    CallerEntidadId = "RCS";
                    break;
            }
        }
        void SetGridColumnsAppearance()
        {
            switch (CallerEntidadId.ToLower().Trim())
            {
                case "recibos":
                    ultraGridData.DisplayLayout.Bands[0].Columns["Recibos"].Hidden = false;
                    break;
                case "rcs":
                    ultraGridData.DisplayLayout.Bands[0].Columns["RCS"].Hidden = false;
                    break;
            }
        }
        protected override void LoadData()
        {
            if (agmEnvioData != null)
                agmEnvioData.Clear();

            IAgmService agmService = AfcaServiceProvider.GetAgmService(); ;

            AgmEnvioData loadedData=null;

            switch (CallerEntidadId.ToLower().Trim())
            {
                case "recibos":
                    loadedData  = agmService.GetAgmEnviosToSendData("Recibos");
                    break;
                case "rcs":
                    loadedData = agmService.GetAgmEnviosToSendData("RCS");
                    break;
            }

            if (loadedData != null)
            {
                agmEnvioData.Merge(loadedData);
                this.ultraGridData.DataSource = agmEnvioData;
                this.ultraGridData.DataMember = agmEnvioData.Ganaderia.TableName;
                this.ultraGridData.DataBind();
            }

        }
        void CheckSelected()
        {
            if (ultraGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow grow in ultraGridData.Selected.Rows)
                    CheckRegistry(grow);

                ultraGridData.UpdateData();
            }
        }
        void CheckRegistry(UltraGridRow grow)
        {
            if (grow.Cells["Status"].Value.ToString() == "PE" ||
                        grow.Cells["Status"].Value.ToString() == "ER")
            {
                grow.Cells["Selected"].Value = true;

                if (grow.Band.Index == 1
                    && grow.ParentRow.Cells["Status"].Value.ToString() == "OK")
                    grow.ParentRow.Cells["Selected"].Value = true;
            }
        }
        void UncheckSelected()
        {
            if (ultraGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow grow in ultraGridData.Selected.Rows)
                    UncheckRegistry(grow);

                ultraGridData.UpdateData();
            }
        }
        void UncheckRegistry(UltraGridRow grow)
        {
            if (grow.Cells["Status"].Value.ToString() == "PE")
                grow.Cells["Selected"].Value = false;
        }
        async Task SendData()
        {
            ultraGridData.UpdateData();

            IAgmService agmService = AfcaServiceProvider.GetAgmService();

            var ganaderiasToSend = agmEnvioData.Ganaderia.Where(g => g.Selected);

            foreach (var ganaderia in ganaderiasToSend)
            {

                //this.Cursor = Cursors.WaitCursor;
                var entidades = agmEnvioData.Detail
                    .Where(d => d.GanaderiaID == ganaderia.GanaderiaID && d.Selected);

                if (entidades != null && entidades.Count() > 0)
                {
                    UltraGridRow gRow = ultraGridData.Rows
                        .Where(r => (string)r.Cells["GanaderiaID"].Value == ganaderia.GanaderiaID).FirstOrDefault();

                    if (gRow != null)
                        currentSendingRow = gRow;
                    else
                        currentSendingRow = null;

                    ArrayList entidadesIncluded = new ArrayList();
                    entidadesIncluded.AddRange(agmEnvioData.Detail
                    .Where(d => d.GanaderiaID == ganaderia.GanaderiaID && d.Selected)
                    .Select(e => e.Entidad).ToArray());

                    string message = agmService
                       .GenerateJsonMessageEnvio(ganaderia.GanaderiaID, entidadesIncluded);

                    if (!String.IsNullOrEmpty(message))
                    {
                        string status = "";

                        try
                        {

                            Action setProgressAction = ShowUIProgressAction;
                            uiTask = Task.Run(setProgressAction);

                            var result = await agmService.SendJsonMessageEnvio(ganaderia.GanaderiaID, message).ConfigureAwait(false);
                            status = result.success ? "OK" : "ER";
                            ganaderia.Message = result.success ? "Envio aceptado" : result.value;

                            if (result.success)
                            {
                                string err = "";
                                var marked = agmService.MarkAgmEnviosAsSended(ganaderia.GanaderiaID, entidadesIncluded, out err);

                                if (!marked)
                                {
                                    ganaderia.Message +=
                                        "\n Envio realizado,sin embargo se ha producido un error al actualizar el estado como enviada en la base de datos local." +
                                        "\n" + err;
                                    status = "ER";
                                }
                            }
                        }
                        catch (System.Exception e)
                        {
                            if (String.IsNullOrEmpty(status))
                                status = "ER";
                            ganaderia.Message += "\n" + e.InnerException;
                        }
                        finally
                        {
                            ganaderia.Status = status;
                            ganaderia.Selected = false;

                            foreach (var entidad in entidades)
                            {
                                entidad.Status = status;
                                entidad.Selected = false;
                            }
                        }


                    }
                    else
                    {
                        ganaderia.Message = "Sin información asociada para enviar.";
                    }
                }

                Action disableProgressAction = HideUIProgressAction;
                uiTask = Task.Run(disableProgressAction);
                uiTask.Wait();
            }
            // prepare json data ; parameters : ganaderiaId, entidades selected
            // send data 
            // if ( ok)  -> mark as updated in database; change status in dataset
            // if (!ok)  -> do nothing in database (log de operaciones?), change status in dataset (show message)            
        }
        void ShowUIProgressAction()
        {
            ShowCursor();
            MarkDataGridRowAsBeingSending();
        }
        void ShowCursor()
        {
            if (this.InvokeRequired)
            {
                Action doMethod = ShowUIProgressAction;
                this.Invoke(doMethod);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
            }
        }
        void MarkDataGridRowAsBeingSending()
        {
            if (this.ultraGridData.InvokeRequired)
            {
                Action doMethod = MarkDataGridRowAsBeingSending;
                this.Invoke(doMethod);
            }
            else
            {
                if (currentSendingRow != null)
                {
                    currentSendingRow.Tag = currentSendingRow.Appearance.BackColor;
                    currentSendingRow.Appearance.BackColor = Color.Orange;
                }
            }
        }
        void HideUIProgressAction()
        {
            HideCursor();
            MarkDataGridRowSendedResult();
        }
        void HideCursor()
        {
            if (this.InvokeRequired)
            {
                Action doMethod = HideUIProgressAction;
                this.Invoke(doMethod);
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        void MarkDataGridRowSendedResult()
        {
            if (this.ultraGridData.InvokeRequired)
            {
                Action doMethod = MarkDataGridRowSendedResult;
                this.Invoke(doMethod);
            }
            else
            {
                if (currentSendingRow != null)
                {
                    string status = currentSendingRow.Cells["Status"].Value.ToString();

                    switch (status.ToUpper())
                    {
                        case "PE":
                            if (currentSendingRow.Tag is Color)
                                currentSendingRow.Appearance.BackColor = (Color)currentSendingRow.Tag;
                            break;
                        case "OK":
                            currentSendingRow.Appearance.BackColor = Color.LightGreen;
                            break;
                        case "ER":
                            currentSendingRow.Appearance.BackColor = Color.LightSalmon;
                            break;
                    }
                }
            }
        }
        private async void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "tbbCheckSelected":
                    CheckSelected();
                    break;
                case "tbbUncheckSelected":
                    UncheckSelected();
                    break;
                case "tbbSend":
                    await SendData();
                    break;
            }
        }
    }
}
