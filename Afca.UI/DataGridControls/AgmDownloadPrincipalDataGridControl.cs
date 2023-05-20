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
using dO = FrameworkForge.Common.DataOperations;

namespace Afca.UI
{
    public partial class AgmDownloadPrincipalDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        FileTracer fileTracer;
        ArrayList traceBuffer = new ArrayList();
        bool traceBufferFlushed = false;
        List<string> entidadesToDownload = new List<string>();

        UltraGridRow currentSavingRow;

        public AgmDownloadPrincipalDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            fileTracer = new FileTracer("agm_down_prin");
            InitializeData();
            LoadData();
        }
        void InitializeData()
        {
            this.entidadesToDownload.Add("RCS");            
        }
        protected override void InitializeAppearance()
        {
            base.InitializeAppearance();
            base.InitializeAppearance();
            ControlsThemeBuilder.GridMakeUp(this.ultraGridData, ThemeColor.Amarillo);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0], ThemeColor.Amarillo);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[1], ThemeColor.Gris);

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
        protected override  async void LoadData()
        {
            if (agmDownloadData != null)
                agmDownloadData.Clear();

            await DownloadData();
            itemContainer.UserInterface.RefreshUI();
        }
        string AppendTraceText(string text,string appendedText)
        {
            appendedText = string.Format("{0} >> {1}", DateTime.Now.ToString("yyyyMMdd hhmmss"),appendedText);
            return string.Format("{0}{1}{2}", text, Environment.NewLine, appendedText);
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
        async Task DownloadData()
        {
            ultraGridData.UpdateData();

            IAgmService agmService = (IAgmService)Service;

            if (entidadesToDownload.Count() > 0)
                WriteTraceBuffer();

            traceBufferFlushed = false;

            foreach (string entidad in entidadesToDownload)
            {

                string traceText = "Starting trace";

                traceText = AppendTraceText(traceText,string.Format("Entidad : {0}. downloading data...", entidad));

                AgmDownloadData entidadDownloaded = await ((IAgmService)Service).DownloadAgmPrincipalData(entidad);

                if(entidadDownloaded!=null && entidadDownloaded.EntidadData.Rows.Count > 0)
                {
                    traceText =AppendTraceText(traceText,string.Format("Entidad : {0}. downloaded data !!",entidad));

                    agmDownloadData.Merge(entidadDownloaded);

                    UltraGridRow gRow = ultraGridData.Rows
                       .Where(r => (string)r.Cells["Entidad"].Value == entidad).FirstOrDefault();

                    if (gRow != null)
                        currentSavingRow = gRow;
                    else
                        currentSavingRow = null;

                    string status = "";

                    if (currentSavingRow == null)                    
                        traceText = AppendTraceText(traceText, string.Format("Entidad : {0}. no data to save", entidad));                    
                    else
                    {
                        string message = dO.cls(gRow, "Message", String.Empty).ToString();

                        if (String.IsNullOrEmpty(message))                        
                            traceText = AppendTraceText(traceText, string.Format("Entidad : {0}. no message to save", entidad));
                        else
                        {
                            try
                            {
                                traceText = AppendTraceText(traceText, string.Format("Entidad : {0}.  message to save", entidad));
                                traceText = AppendTraceText(traceText, string.Format("{0}", message));

                                await Task.Run(() => ShowUIProgressAction(currentSavingRow));

                                Result result = await agmService.SaveJsonDownloadedData(entidad,message).ConfigureAwait(false);

                                status = result.success ? "OK" : "ER";
                                currentSavingRow.Cells["Result"].Value = result.success ? "Descarga guardada" : result.value;

                                if (result.success)
                                {
                                    traceText = AppendTraceText(traceText, string.Format("Entidad : {0}.  message saved", entidad));

                                    DateTime fechaDescarga= Convert.ToDateTime(currentSavingRow.Cells["FechaDescarga"].Value);

                                    var notified = await agmService.NotifyDescargaAsRecieved(entidad,fechaDescarga);

                                    if (!notified.success)
                                    {
                                        currentSavingRow.Cells["Result"].Value +=
                                            "\n Descarga realizada y almacenada,sin embargo se ha producido un error al notificar al servidor central su recepción." +
                                            "\n" + notified.value;
                                        status = "ER";
                                        traceText = AppendTraceText(traceText, string.Format("Entidad : {0}. ERROR when sending ACK to central server", entidad));
                                    }
                                    else                                    
                                        traceText = AppendTraceText(traceText, string.Format("Entidad : {0}. ACK sended to central server", entidad));                                   
                                }
                                else
                                {

                                    currentSavingRow.Cells["Result"].Value +=
                                            "\n descarga realizada,sin embargo se ha producido un error al intentar guardarla en la base de datos local.";                                          
                                        status = "ER";   
                                }
                            }
                            catch (System.Exception e)
                            {
                                if (String.IsNullOrEmpty(status))
                                    status = "ER";
                                currentSavingRow.Cells["Result"].Value += "\n" + e.InnerException;
                            }
                            finally
                            {
                                currentSavingRow.Cells["Status"].Value = status;
                                await Task.Run(() => HideUIProgressAction(currentSavingRow));
                            }
                        }
                    }
                }
                traceBuffer.Add(traceText);                
            }         
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
                    gridRow.Tag = currentSavingRow.Appearance.BackColor;
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
                        case "PS":
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
    }
}
