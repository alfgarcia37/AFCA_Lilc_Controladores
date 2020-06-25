using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Afca.Common;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
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
            string codGanaderia = selectedRow.Cells["CodigoGanaderia"].Value.ToString()
                 , codCrotal = selectedRow.Cells["CodigoCrotal"].Value.ToString();
            service.SetImagenCartaImprimida(codGanaderia, codCrotal);
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
        void ExportarSiluetasSeleccionadas()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                string folderToSave = "";
                ArrayList errores = new ArrayList();

                folderToSave = dialog.SelectedPath;

                int total=0, actualizadas=0;

                foreach (UltraGridRow selectedRow in gridSiluetas.Selected.Rows)
                {
                    try
                    {
                        var imageBytes = LoadImagenCarta(
                         selectedRow.Cells["CodigoGanaderia"].Value.ToString()
                        , selectedRow.Cells["CodigoCrotal"].Value.ToString());

                        if (imageBytes != null)
                        {
                            total++;
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            var image = Image.FromStream(ms, true);
                            image.Save(string.Format(@"{0}\{1}.png", folderToSave, selectedRow.Cells["CodigoCrotal"].Value.ToString())
                                , ImageFormat.Png);
                            SetImagenCartaImprida(selectedRow);
                            selectedRow.Cells["Imprimida"].Value = DateTime.Now;
                            actualizadas++;
                        }
                    }
                    catch (Exception e)
                    {
                        errores.Add(string.Format("{0} : {1}"
                            ,selectedRow.Cells["CodigoCrotal"].Value.ToString(),e.Message));
                    }
                }
                Cursor = Cursors.Default;

                if (total > 0)
                {
                    DialogsManager.ShowInfo("Resultado",
                        string.Format(@"Se han exportado corretamente {0}\{1} siluetas",actualizadas,total));
                }

                if (total>0 && errores.Count > 0)
                {
                    DialogsManager.ShowErrorDialog("Exportación siluetas",
                        string.Format("Se han producido los siguientes errores al exportar:{0}{1}"
                        , Environment.NewLine
                        , String.Join(Environment.NewLine, errores.ToArray())));
                }
                Process.Start("explorer", folderToSave);
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
            if (e.Type.Equals(typeof(UltraGridRow)))
            {
                var selectedRow = gridSiluetas.Selected.Rows[0];
                PreviewImagenCarta(selectedRow);
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
                    ExportarSiluetasSeleccionadas();
                    break;
            }
        }
        #endregion


    }
}
