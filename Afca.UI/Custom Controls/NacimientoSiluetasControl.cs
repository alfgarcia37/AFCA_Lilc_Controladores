using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Afca.Common;
using Afca.Common.DataReports;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
using Afca.Reports;
using CrystalDecisions.CrystalReports.Engine;
using FrameworkForge.Common;
using Infragistics.Win.UltraWinGrid;
using dO = FrameworkForge.Common.DataOperations;

namespace Afca.UI
{
    public partial class NacimientoSiluetasControl : UserControl
    {
        #region variables
        INacimientoSiluetasService service;
        NacimientoSiluetasQueryData queryData;
        #endregion

        #region Constructor & métodos de inicio
        public NacimientoSiluetasControl()
        {
            InitializeComponent();
            InitializeServices();
            InitializeDefaultValues();
            InitializeMakeUp();
            LoadData();
        }
        void InitializeMakeUp()
        {
            ControlsThemeBuilder.GridMakeUp(gridSiluetas, ThemeColor.Verde);
            ControlsThemeBuilder.GridBandMakeUp(gridSiluetas.DisplayLayout.Bands[0], ThemeColor.Verde);
        }
        void InitializeServices()
        {
            service = AfcaServiceProvider.GetNacimientoSiluetasService();
        }
        void InitializeDefaultValues()
        {
            udteCreadoDesde.Value =
                DateTime.Now.AddMonths(-1);
        }

        #endregion

        #region métodos consulta
        void LoadData()
        {
            if (FillQueryData())
            {
                NacimientoSiluetasData loadedData =
                    service.LoadNacimientoSiluetasDataByConsulta(queryData);
                nacimientoSiluetasData.Clear();
                nacimientoSiluetasData.Merge(loadedData);
                gridSiluetas.DataSource = nacimientoSiluetasData;
                gridSiluetas.DataMember = nacimientoSiluetasData.NacimientoSiluetas.TableName;
                gridSiluetas.DataBind();
            }
            else
                DialogsManager.ShowInfo("Aviso"
                    , "Ha de rellenar algún parámetro de consulta.");
        }
        void ClearQueryControlsData()
        {
            uteCodigoGanaderia.Clear();
            uteCodigoCrotal.Clear();
            uneControlador.Value = null;
            udteCreadoDesde.Value = DBNull.Value;
            udteCreadoHasta.Value = DBNull.Value;
            udteNacimiento.Value = DBNull.Value;
        }
        bool FillQueryData()
        {
            //fillquery data from controlsvalues
            if (queryData == null)
                queryData = new NacimientoSiluetasQueryData();

            queryData.Clear();
            DataRow queryRow = queryData.Query.NewRow();

            if (!String.IsNullOrEmpty(uteCodigoGanaderia.Text))
                queryRow["CodigoGanaderia"] = uteCodigoGanaderia.Text;

            if (!String.IsNullOrEmpty(uteCodigoCrotal.Text))
                queryRow["CodigoCrotal"] = uteCodigoCrotal.Text;

            if (!dO.EsNulo(uneControlador.Value))
                queryRow["Controlador"] = uneControlador.Value;

            if (!dO.EsNulo(udteCreadoDesde.Value))
                queryRow["FechaCreadoDesde"] = udteCreadoDesde.Value;

            if (!dO.EsNulo(udteCreadoHasta.Value))
                queryRow["FechaCreadoHasta"] = udteCreadoHasta.Value;

            if (!dO.EsNulo(udteNacimiento.Value))
                queryRow["FechaNacimiento"] = udteNacimiento.Value;

            queryData.Query.Rows.Add(queryRow);
            return queryRow.ItemArray.Where(o => !dO.EsNulo(o)).Count() > 0;
        }
        #endregion

        #region métodos pintado & exportado imágenes
        void PreviewImagenCarta(UltraGridRow selectedRow)
        {
            var imageBytes = LoadImagenCarta(
                 selectedRow.Cells["CodigoGanaderia"].Value.ToString()
                , selectedRow.Cells["CodigoCrotal"].Value.ToString());

            if (imageBytes != null)
            {
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                var image = Image.FromStream(ms, true);
                ultraPictureBox.Image = image;
            }
            else
                ultraPictureBox.Image = null;
        }
        byte[] LoadImagenCarta(string argCodigoGanaderia, string argCodigoCrotal)
        {
            var image = service.GetImageCarta(argCodigoGanaderia, argCodigoCrotal);

            if (image != null)
                return (byte[])image;

            return null;
        }
        void SetImagenCartaImprida(UltraGridRow selectedRow)
        {
            service.SetImagenCartaImprimida(
                selectedRow.Cells["CodigoGanaderia"].Value.ToString(),
                new string[] {selectedRow.Cells["CodigoCrotal"].Value.ToString()
                            ,dO.cls(selectedRow, "CodigoConafe", "").ToString()
                            ,dO.cls(selectedRow, "OrdenTrabajo", "").ToString()});
        }
        void SetRowSelectorIconAppearance(UltraGridRow row)
        {
            string icon = "";
            icon = !dO.EsNulo(row, "Imprimida") ? "IconoImprimida" : "IconoNoImprimida";
            row.RowSelectorAppearance.Image = FrameworkForge.Common.ResourceProvider.GetIconImage(icon);

            if (dO.EsNulo(row, "Imprimida"))
            {
                row.Tag = row.Appearance.ForeColor;
                row.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (row.Cells["Imprimida"].OriginalValue
                    != row.Cells["Imprimida"].Value && row.Tag != null)
                    row.Appearance.ForeColor = (Color) row.Tag;
            }

        }
        void SaveInfo()
        {
            if (nacimientoSiluetasData.NacimientoSiluetas.GetChanges() != null)
            {
                try
                {
                    var data = (NacimientoSiluetasData)nacimientoSiluetasData.GetChanges();
                    service.UpdateNacimientoSiluetasData(data);
                    nacimientoSiluetasData.AcceptChanges();
                }
                catch (System.Exception e)
                {
                    DialogsManager.ShowErrorDialog("Guardando info....",
                       string.Format("Se han producido el siguiente error al guardar:{0}"
                       , e.Message));
                }
            }
        }
        void ExportarCartasSeleccionadas()
        {
            Cursor = Cursors.WaitCursor;
            string folderToSave = ConfigurationProvider.GetConfigurationValue("RutaImpresoraCarta");

            ArrayList errores = new ArrayList();
            int total = 0, actualizadas = 0;

            foreach (UltraGridRow selectedRow in gridSiluetas.Selected.Rows)
            {
                try
                {
                    string codCrotal = dO.cls(selectedRow, "CodigoCrotal", "").ToString()
                       , codGanaderia = dO.cls(selectedRow, "CodigoGanaderia", "").ToString()
                       , codConafe = dO.cls(selectedRow, "CodigoConafe", "").ToString();

                    if (!String.IsNullOrEmpty(codCrotal) && !String.IsNullOrEmpty(codGanaderia))
                    {
                        CartaRegistroReportData data = LoadCartaRegistroData(codGanaderia, codCrotal);

                        if (data != null)
                        {
                            total++;

                            CartaRegistroReport report = new CartaRegistroReport();
                            report.SetDataSource(data);

                            if (String.IsNullOrEmpty(codConafe))
                                throw new Exception(string.Format("El crotal {0} no tiene codigo de CONAFE asignado", codCrotal));

                            PrintCartaTiff(report, codConafe.Substring(codConafe.Length - 8, 8));
                            Thread.Sleep(1500);

                            SetImagenCartaImprida(selectedRow);

                            selectedRow.Cells["Imprimida"].Value = DateTime.Now;
                            (selectedRow.ListObject as DataRowView).Row.AcceptChanges();
                            actualizadas++;
                        }
                    }
                }
                catch (Exception e)
                {
                    errores.Add(string.Format("{0} : {1}"
                        , selectedRow.Cells["CodigoCrotal"].Value.ToString(), e.Message));
                }
            }

            Cursor = Cursors.Default;

            //Chequear que el fichero no tenga ninguna protección de lectura
            //En el equipo de JoseLuis no cambia el nombre al último fichero imprimido
            if (folderToSave != "")
            {
                DirectoryInfo info = new DirectoryInfo(folderToSave);

                FileInfo[] files = info.GetFiles("*Crystal*");

                for (int i = files.Length - 1; i >= 0; i--)
                {
                    string[] split = files[i].Name.Split(new char[] { '-' });

                    if (split.Length == 2)
                    {
                        if (File.Exists(string.Format(@"{0}\{1}", folderToSave, split[1])))
                            File.Delete(string.Format(@"{0}\{1}", folderToSave, split[1]));

                        File.Move(files[i].FullName,
                            string.Format(@"{0}\{1}", folderToSave, split[1]));

                        //File.Copy(files[i].FullName,
                        //    string.Format(@"{0}\{1}", folderToSave, split[1]), true);
                        //File.Delete(files[i].FullName);
                    }
                }
            }

            if (total > 0)
            {
                DialogsManager.ShowInfo("Resultado",
                    string.Format(@"Se han exportado correctamente {0}\{1} cartas", actualizadas, total));
            }
            if (total > 0 && errores.Count > 0)
            {
                DialogsManager.ShowErrorDialog("Exportación cartas",
                    string.Format("Se han producido los siguientes errores al exportar:{0}{1}"
                    , Environment.NewLine
                    , String.Join(Environment.NewLine, errores.ToArray())));
            }

            Process.Start("explorer", folderToSave);

        }
        void PrintCartaTiff(CartaRegistroReport report,string fileTitle)
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = "PDFCreator";            

            PageSettings pgs = new PageSettings();
            pgs.PrinterSettings = ps;
            pgs.PrinterSettings.PrintFileName = fileTitle;

            ((ReportClass)report).SummaryInfo.ReportTitle = fileTitle;
            ((ReportClass)report).PrintToPrinter(ps, pgs, false);
        }
    
        void PreviewCartaReport(UltraGridRow selectedRow)
        {
            //previsualizar informe
            try
            {
                string codCrotal = dO.cls(selectedRow,"CodigoCrotal","").ToString()
                    , codGanaderia = dO.cls(selectedRow, "CodigoGanaderia", "").ToString();

                if (!String.IsNullOrEmpty(codCrotal) && !String.IsNullOrEmpty(codGanaderia))
                {
                    CartaRegistroReportData data = LoadCartaRegistroData(codGanaderia, codCrotal);

                    CartaRegistroReport report = new CartaRegistroReport();
                    report.SetDataSource(data);
                    crystalReportViewer.ReportSource = report;
                }
                else
                    DialogsManager.ShowInfo("Mensaje"
                        , "El registro seleccionado no tiene datos para mostrar la carta");
            }
            catch (Exception e)
            {
                DialogsManager.ShowErrorDialog("Mostrar carta"
                    ,string.Format("{0}{1}{2}",e.Message
                    ,Environment.NewLine,e.InnerException!=null?e.InnerException.Message:""));
            }
        }
        CartaRegistroReportData LoadCartaRegistroData(string argCodigoGanaderia,string argCodigoCrotal)
        {
            try
            {
                CartaRegistroReportData data = service.GetCartaRegistroData(argCodigoGanaderia, argCodigoCrotal);
                ChangeFirmaBackground(data);
                return data;
            }
            catch (Exception e)
            {
                throw new Exception("Error al cargar los datos de la carta",e);
            }
        }
        #endregion

        #region Eventos
        private void utbtnConsultar_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void gridSiluetas_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
        }
        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            if (gridSiluetas.Selected.Rows.Count == 0) return;

            if (e.Tab.Key=="tbbCarta")
            {
                Cursor = Cursors.WaitCursor;
                var selectedRow = gridSiluetas.Selected.Rows[0];
                PreviewCartaReport(selectedRow);
                Cursor = Cursors.Default;
            }
            else if(e.Tab.Key=="tbbSilueta")
            {
                Cursor = Cursors.WaitCursor;
                var selectedRow = gridSiluetas.Selected.Rows[0];
                PreviewImagenCarta(selectedRow);
                Cursor = Cursors.Default;
            }
            
        }
        private void gridSiluetas_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            SetRowSelectorIconAppearance(e.Row);
        }
        private void gridSiluetas_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Imprimida")
                SetRowSelectorIconAppearance(e.Cell.Row);
        }
        private void utbtnLimpiar_Click(object sender, EventArgs e)
        {
            ClearQueryControlsData();
        }
        private void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "tbbExportar":
                    ExportarCartasSeleccionadas();
                    break;
                case "tbbGuardar":
                    SaveInfo();
                    break;
            }
        }

        #endregion

        #region Firma methods
        void ChangeFirmaBackground(CartaRegistroReportData data)
        {
            if (dO.HasRows(data, "Datos"))
            {
                DataRow datosRow = data.Datos.Rows[0];

                if (!dO.EsNulo(datosRow, "Firma"))
                {
                    byte[] firmaOriginal = (byte[]) datosRow["Firma"];
                    datosRow["Firma"] = RemoveTransparentBackground(firmaOriginal);
                }
            }
        }
        byte[] RemoveTransparentBackground(byte[] orgImage)
        {
            using (MemoryStream dest = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(orgImage))
                {
                    Bitmap org = new Bitmap(ms);
                    Bitmap target = new Bitmap(org.Size.Width, org.Size.Height);
                    Graphics g = Graphics.FromImage(target);
                    g.Clear(Color.White);
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    g.DrawImage(org,0, 0,org.Size.Width,org.Size.Height);
                    target.Save(dest, ImageFormat.Png);
                    return dest.ToArray();
                }
            }
        }
        #endregion
    }
}
