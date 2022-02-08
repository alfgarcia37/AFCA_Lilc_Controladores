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
    public partial class AgmEnvioPrincipalDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        Task uiTaskShow;
        Task uiTaskHide;
        FileTracer fileTracer;
        ArrayList traceBuffer = new ArrayList();
        bool traceBufferFlushed = false;

        UltraGridRow currentSendingRow;
        public AgmEnvioPrincipalDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            fileTracer = new FileTracer("agm_env_prin");
            LoadData();
        }
        protected override void InitializeAppearance()
        {
            base.InitializeAppearance();
            ControlsThemeBuilder.GridMakeUp(this.ultraGridData, ThemeColor.Amarillo);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0], ThemeColor.Amarillo);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[1], ThemeColor.Gris);

            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbCheckSelected", "IconoSelectAll");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbUncheckSelected", "IconoUnselectAll");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbSend", "IconoPlay");

            foreach(string tbbkey in new string[] {"tbbAddNew","tbbDelete","tbbSave" })
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
        protected override void LoadData()
        {
            if (agmEnvioData != null)
                agmEnvioData.Clear();

            base.LoadData();
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

            IAgmService agmService = ((IAgmService)Service);

            var ganaderiasToSend = agmEnvioData.Ganaderia.Where(g => g.Selected);

            if (ganaderiasToSend.Count() > 0)
                WriteTraceBuffer();

            traceBufferFlushed = false;

            foreach (var ganaderia in ganaderiasToSend)
            {

                //this.Cursor = Cursors.WaitCursor;
                var entidades = agmEnvioData.Detail
                    .Where(d => d.GanaderiaID == ganaderia.GanaderiaID && d.Selected);

                if (entidades != null && entidades.Count() > 0)
                {
                    string traceText = string.Format(">>GanaderiaID : {0}", ganaderia.GanaderiaID);

                    UltraGridRow gRow = ultraGridData.Rows
                        .Where(r => (string) r.Cells["GanaderiaID"].Value == ganaderia.GanaderiaID).FirstOrDefault();

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
                        
                        traceText = string.Format("{0}{1}>>{2}",traceText,Environment.NewLine,message);

                        try
                        {

                            await Task.Run(() => ShowUIProgressAction(currentSendingRow));

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
                                var marked = agmService.MarkAgmEnvioAsNotSended(ganaderia.GanaderiaID,"Principal",ganaderia.Message,out err);

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

                            traceText = string.Format("{0}{1}<<{2}", traceText,Environment.NewLine, ganaderia.Message);
                        }
                    }
                    else
                    {
                        ganaderia.Message = "Sin información asociada para enviar.";
                        traceText = string.Format("{0}{1}>>{2}",traceText,Environment.NewLine,"Sin información asociada para enviar.");
                    }
                    traceBuffer.Add(traceText);
                }
                await Task.Run(() => HideUIProgressAction(currentSendingRow));
            }
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
                this.Invoke(doMethod, gridRow);
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

            Task.Run(() => MarkDataGridRowSendedResult(gridRow));

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
                this.Invoke(doMethod, gridRow);
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

        private void ultraGridData_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }
    }
}
