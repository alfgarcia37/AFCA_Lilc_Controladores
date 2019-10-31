using Afca.Common.DataSets;
using FrameworkForge.UI;
using FrameworkForge.UI.Interfaces;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using Infragistics.Win.UltraWinTree;
using System;
using System.Data;
using System.Windows.Forms;
using Afca.Common.ServicesInterfaces;
using Afca.Common;
using System.Collections;
using Infragistics.Win.UltraWinDock;
using FrameworkForge.Common.DataSets;
using dO = FrameworkForge.Common.DataOperations;



namespace Afca.UI
{
    public static class ControlsProvider
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ControlsTesterForm());
        }

        public static Hashtable Controls = new Hashtable();
        public static Form ShowControlInForm(string title, string iconName, Control controlAdded)
        {
            Form newForm = new Form();
            newForm.Text = title;
            newForm.Icon = ResourceProvider.GetIcon(iconName);
            newForm.MdiParent = FrameworkForge.UI.ApplicationContext.FrameForm;
            newForm.Controls.Add(controlAdded);
            newForm.Controls[0].Dock = DockStyle.Fill;
            newForm.Show();
            return newForm;
        }
        public static void ShowForm(Form newForm, string iconName)
        {
            newForm.Icon = ResourceProvider.GetIcon(iconName);
            newForm.MdiParent = FrameworkForge.UI.ApplicationContext.FrameForm;
            newForm.Show();
        }

        #region Comunes
        public static void CloseDockedControl(Control control)
        {
            if (control.Tag != null && control.Tag is object[])
            {
                object[] info = (object[])control.Tag;

                if (info.Length == 2 && info[0] is DockableGroupPane)
                {
                    ((AfcaFrameForm)FrameworkForge.UI.ApplicationContext.FrameForm)
                        .CloseControl(info[1].ToString(), (DockableGroupPane)info[0]);
                }
            }
        }
        #endregion

        #region Explorador
        public static void ShowAfcaExplorer()
        {
            TreeViewControl treeView;

            if (ControlsProvider.Controls["Explorer"] == null)
            {
                treeView = new TreeViewControl(Afca.Common.Trees.ExplorerTree, ThemeColor.Azul);
                ControlsProvider.Controls.Add("Explorer", treeView);
            }
            else
            {
                treeView = (TreeViewControl)ControlsProvider.Controls["Explorer"];
            }

            ((AfcaFrameForm)FrameworkForge.UI.ApplicationContext.FrameForm)
                .AddControlToDockPanel(
                            treeView,
                            ((AfcaFrameForm)FrameworkForge.UI.ApplicationContext.FrameForm).DockGroupLeftExplorer,
                            "Explorador",
                            "Explorador",
                            "IconoPaneExplorador");



            ITreeDetailViewsUI detailsView;

            if (ControlsProvider.Controls["ExplorerDetails"] != null)
                detailsView = (ITreeDetailViewsUI)ControlsProvider.Controls["ExplorerDetails"];
            else
            {
                detailsView = ControlsProvider.ShowAfcaExplorerDetails();
            }

            treeView.SetTreeDetailsView(detailsView);

            UltraTreeNode lecturasExplotaciones = treeView.SelectNodeByItemName("LecturasEnProgreso");
            if (lecturasExplotaciones != null)
            {
                lecturasExplotaciones.Expanded = true;
                lecturasExplotaciones.Selected = true;
            }
        }
        public static ITreeDetailViewsUI ShowAfcaExplorerDetails()
        {
            if (ControlsProvider.Controls["ExplorerDetails"] == null)
            {
                TreeDetailsViewGridControl treeDetailView = new TreeDetailsViewGridControl(ThemeColor.Azul);

                Form newForm = ShowControlInForm(
                    "Detalles del explorador de objetos"
                    , "IconoExplorerDetails"
                    , treeDetailView);

                newForm.FormClosed += delegate
                {
                    ControlsProvider.Controls.Remove("ExplorerDetails");
                    ((ITreeDetailViewsUI)treeDetailView).NotifyClosed();
                };

                ControlsProvider.Controls.Add("ExplorerDetails", treeDetailView);

                return (ITreeDetailViewsUI)treeDetailView;
            }
            else
                return (ITreeDetailViewsUI)ControlsProvider.Controls["ExplorerDetails"];

        }
        #endregion

        #region Lecturas
        public static void ShowLecturaControlDataGrid(ItemData itContainer)
        {
            if (itContainer.MetaData.ItemType == ItemType.Leaf)
                ShowControlInForm(
                                "Lecturas"
                                , "IconoControlador"
                                , new LecturaControlDataGridControl(itContainer
                                , int.Parse(itContainer.ItemColumns["CodigoControlador"].Data.Value.ToString())));
            else

                ShowControlInForm(
                    "Lecturas"
                    , "IconoControlador"
                    , new LecturaControlDataGridControl(itContainer));
        }
        public static void ShowLecturaPropertiesByItem(ItemData itLectura)
        {
            string estadoId =
                itLectura.ItemColumns["Estado"].Data.Value.ToString();

            ShowControlInForm(
                itLectura.ItemColumns["EstabloNombre"].Data.Value.ToString()
                , IconMapper.GetAfcaIconName(
                    (EstadoLectura)int.Parse(estadoId))
                , new LecturaPropertiesControl(itLectura));
        }
        public static void ShowLecturaProperties(DataRow lecturaRow)
        {
            string estadoId =
                lecturaRow["Estado"].ToString();

            ShowControlInForm(
                lecturaRow["EstabloNombre"].ToString()
                , IconMapper.GetAfcaIconName(
                    (EstadoLectura)int.Parse(estadoId))
                , new LecturaPropertiesControl(lecturaRow["Establo"].ToString(),estadoId));
        }
        public static void SetLecturaStateToPlanificar(ItemData itLectura)
        {
            try
            {
                SetLecturaState(itLectura.ItemColumns["Establo"].Data.Value.ToString()
                     , (int)EstadoLectura.Planificar);
                itLectura.UserInterface.RefreshUI();
                DialogsManager.ShowSuccessInfo("Resultado", "Estado actualizado correctamente!");
            }
            catch (System.Exception e)
            {
                DialogsManager.ShowErrorDialog("Resultado",
                    e.Message);
            }
        }
        public static void SetLecturaStateToEnCurso(ItemData itLectura)
        {
            try
            { 
            SetLecturaState(itLectura.ItemColumns["Establo"].Data.Value.ToString()
                 , (int)EstadoLectura.EnCurso);
            itLectura.UserInterface.RefreshUI();
            DialogsManager.ShowSuccessInfo("Resultado", "Estado actualizado correctamente!");
            }
            catch (System.Exception e)
            {
                DialogsManager.ShowErrorDialog("Resultado",
                    e.Message);
            }
        }
        public static void SetLecturasSiguienteLote(ItemData item)
        {

            int codigoControlador = -1;

            if (item.ItemColumns.ContainsKey("NumControler"))
                codigoControlador = int.Parse(item.ItemColumns["NumControler"].Data.Value.ToString());
            else if (((ItemData)item.Parent).ItemColumns.ContainsKey("CodigoControlador"))
                codigoControlador =
                    int.Parse(((ItemData)item.Parent).ItemColumns["CodigoControlador"].Data.Value.ToString());

            SetLecturasSiguienteLoteByID(codigoControlador);

        }
        public static void SetLecturasSiguienteLoteByID(int codigoControlador)
        {
            try
            {               
                IControladorService controladorSrv = AfcaServiceProvider.GetControladorService();
                controladorSrv.PrepareNextLecturasLote(codigoControlador);
                DialogsManager.ShowSuccessInfo("Resultado", "Lote generado correctamente!");
            }
            catch (System.Exception e)
            {
                DialogsManager.ShowErrorDialog("Resultado",
                    e.Message);
            }
        }
        public static void SetLecturaState(string argEstablo,int estado)
        {
            ILecturaControlService lecturaControlSrv= AfcaServiceProvider.GetLecturaControlService();
            lecturaControlSrv.UpdateLecturaControlEstado(argEstablo, estado);
        }
        public static void GenerarLecturasControlador(ItemData item)
        {

            if (item.ItemColumns.ContainsKey("CodigoControlador"))
                GenerarLecturasControladorByID(int.Parse(item.ItemColumns["CodigoControlador"].Data.Value.ToString()));
            else
                return;
        }
        public static void GenerarLecturasControladorByID(int codigoControlador)
        {
            try
            {
                IGenerarService iSvrGenerar = AfcaServiceProvider.GetGenerarService();

                Result result = iSvrGenerar.CheckToXmlData(codigoControlador);
                if (result.success == false)
                {
                    DialogResult dr = MessageBox.Show(result.value + "\n" + "No coinciden Maestro y Control"
                                                , "CHECK Maestro y Control", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No) return;
                }

                Result resultado = iSvrGenerar.GenerarXMLs(codigoControlador);

                if (resultado.success == true)
                    iSvrGenerar.PasarXmlToUP(codigoControlador);

                if (resultado.success == true)
                    DialogsManager.ShowSuccessInfo("Proceso Exportación", "Exportación correcta!");

            }
            catch (System.Exception e)
            {
                DialogsManager.ShowErrorDialog("Error",
                    e.Message);
            }
        }
        public static void ImportarLecturasControlador(ItemData item)
        {

            if (item.ItemColumns.ContainsKey("CodigoControlador"))
                ImportarLecturasControladorByID(int.Parse(item.ItemColumns["CodigoControlador"].Data.Value.ToString()));
            else
                return;
        }
        public static DataSet ImportarLecturasControladorByID(int codigoControlador)
        {
            try
            {
                IImportarService iSvrImportar;
                iSvrImportar = AfcaServiceProvider.GetImportarService();

                DataSet incidenciasData = iSvrImportar.PasarDownToXml(codigoControlador);

                if(incidenciasData!=null && dO.HasRows(incidenciasData, "Incidencia"))
                {
                    DialogsManager.ShowErrorDialog("Error", string.Format("Se han producido errores en el proceso de importacion.{0}{1}"
                        , Environment.NewLine, "Abra la ventana de edición de lecturas y revíselos, por favor."));
                }

                return incidenciasData;
                //sigue siendo necesario el código de abajo??
                /*
                Result result1 = iSvrImportar.CheckFromXmlData(codigoControlador);
                if (result1.success == false)
                {
                    DialogsManager.ShowErrorDialog("Error", string.Format("Se han producido los siguientes errores:{0}{1}"
                        ,Environment.NewLine,result1.value));
                    return incidenciasData;
                }

                Result resultado1 = iSvrImportar.ImportarXMLs(codigoControlador);
                if (resultado1.success == true)
                {
                    DialogResult dr3 = MessageBox.Show(resultado1.value + "\n" + "Importación OK", "proceso Importación", MessageBoxButtons.OK);
                    resultado1 = iSvrImportar.GetXmlActualizacionesGlobalData(codigoControlador);
                }
                */
            }
            catch (System.Exception e)
            {
                DialogsManager.ShowErrorDialog("Error",
                    e.Message);
                return null;
            }
        }

        #endregion

    }

}


