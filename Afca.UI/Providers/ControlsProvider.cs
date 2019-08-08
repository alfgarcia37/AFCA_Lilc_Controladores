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

            UltraTreeNode lecturasExplotaciones = treeView.SelectNodeByItemName("Lecturas");
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
        #endregion

    }

}


