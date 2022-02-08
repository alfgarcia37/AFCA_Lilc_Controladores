using Afca.Common;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
using Afca.Common.Utilities;
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
        Task uiTaskShow;
        Task uiTaskHide;

        FileTracer fileTracer;
        ArrayList traceBuffer = new ArrayList();
        bool traceBufferFlushed = false;

        UltraGridRow currentSendingRow;
        string CallerEntidadId;

        public AgmEnvioSecundarioDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            SetCallerEntidadId();
            fileTracer = new FileTracer(string.Format("agm_env_{0}",CallerEntidadId));
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
                case "AgmEnviosCalificaciones":
                    CallerEntidadId = "Calificaciones";
                    break;
                case "AgmEnviosCenso":
                    CallerEntidadId = "Censo";
                    break;
                case "AgmEnviosGenomica":
                    CallerEntidadId = "Genomica";
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
                case "calificaciones":
                    ultraGridData.DisplayLayout.Bands[0].Columns["Calificaciones"].Hidden = false;
                    break;
                case "censo":
                    ultraGridData.DisplayLayout.Bands[0].Columns["Censo"].Hidden = false;
                    break;
                case "genomica":
                    ultraGridData.DisplayLayout.Bands[0].Columns["Genomica"].Hidden = false;
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
                case "calificaciones":
                    loadedData = agmService.GetAgmEnviosToSendData("Calificaciones");
                    break;
                case "censo":
                    loadedData = agmService.GetAgmEnviosToSendData("Censo");
                    break;
                case "genomica":
                    loadedData = agmService.GetAgmEnviosToSendData("Genomica");
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
        void WriteTraceBuffer()
        {
            if (!traceBufferFlushed)
            {
                if (traceBuffer.Count > 0)
                {
                    foreach (string msg in traceBuffer)
                        fileTracer.AppendTraceText(msg);

                    traceBuffer.Clear();
                    traceBufferFlushed = true;
                }
            }
        }
        async Task SendData()
        {
            ultraGridData.UpdateData();

            IAgmService agmService = AfcaServiceProvider.GetAgmService();

            var ganaderiasToSend = agmEnvioData.Ganaderia.Where(g => g.Selected);

            if (ganaderiasToSend.Count() > 0)
                WriteTraceBuffer();

            traceBufferFlushed = false;

            foreach (var ganaderia in ganaderiasToSend)
            {

                var entidades = agmEnvioData.Detail
                    .Where(d => d.GanaderiaID == ganaderia.GanaderiaID && d.Selected);

                if (entidades != null && entidades.Count() > 0)
                {
                    string traceText = string.Format(">>GanaderiaID : {0}", ganaderia.GanaderiaID);

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

                        traceText = string.Format("{0}{1}>>{2}", traceText, Environment.NewLine, message);


                        try
                        {
                            await Task.Run(()=> ShowUIProgressAction(currentSendingRow));

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
                            else
                            {
                                string err = "";
                                var marked = agmService.MarkAgmEnvioAsNotSended(ganaderia.GanaderiaID,CallerEntidadId, ganaderia.Message, out err);

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

                            traceText = string.Format("{0}{1}<<{2}", traceText, Environment.NewLine, ganaderia.Message);

                        }
                    }
                    else
                    {
                        ganaderia.Message = "Sin información asociada para enviar.";
                        traceText = string.Format("{0}{1}>>{2}", traceText, Environment.NewLine, "Sin información asociada para enviar.");                        
                    }
                    traceBuffer.Add(traceText);
                }
                await Task.Run(()=>HideUIProgressAction(currentSendingRow));
            }

            WriteTraceBuffer();
            HideCursor();
           
        }
        void ShowUIProgressAction(UltraGridRow gridRow)
        {
            Action showCursorAct = ShowCursor;
            Task t1 = Task.Run(showCursorAct);

            Task.Run(() => MarkDataGridRowAsBeingSending(gridRow));
        }   
        void ShowCursor()
        {
            if (this.InvokeRequired)
            {
                Action doMethod = ShowCursor;
                this.Invoke(doMethod);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
            }
        }
        void MarkDataGridRowAsBeingSending(UltraGridRow gridRow)
        {
            if (this.ultraGridData.InvokeRequired)
            {
                Action<UltraGridRow> doMethod = MarkDataGridRowAsBeingSending;
                this.Invoke(doMethod,gridRow);                
            }
            else
            {
                if (gridRow != null)
                {
                    gridRow.Tag = currentSendingRow.Appearance.BackColor;
                    gridRow.Appearance.BackColor = Color.Orange;
                    gridRow.Activate();
                    gridRow.Refresh();
                }
            }
        }
        void HideUIProgressAction(UltraGridRow gridRow)
        {
            Task[] tasksHide = new Task[2];

            Action hideCursorAct = HideCursor;
            Task t1 = Task.Run(hideCursorAct);

            Task.Run(()=>MarkDataGridRowSendedResult(gridRow));

        }
        void HideCursor()
        {
            if (this.InvokeRequired)
            {
                Action doMethod = HideCursor;
                this.Invoke(doMethod);
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        void MarkDataGridRowSendedResult(UltraGridRow gridRow)
        {
            if (this.ultraGridData.InvokeRequired)
            {
                Action<UltraGridRow> doMethod = MarkDataGridRowSendedResult;
                this.Invoke(doMethod,gridRow);
            }
            else
            {
                if (gridRow != null)
                {
                    string status = gridRow.Cells["Status"].Value.ToString();

                    switch (status.ToUpper())
                    {
                        case "PE":
                            if (gridRow.Tag is Color)
                                gridRow.Appearance.BackColor = (Color)gridRow.Tag;
                            break;
                        case "OK":
                            gridRow.Appearance.BackColor = Color.LightGreen;
                            break;
                        case "ER":
                            gridRow.Appearance.BackColor = Color.LightSalmon;
                            break;
                    }
                    gridRow.Refresh();
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
                    itemContainer.UserInterface.RefreshUI();
                    break;
            }
        }
    }
}
