using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrameworkForge.UI;
using FrameworkForge.Common;
using FrameworkForge.Common.Generics;
using FrameworkForge.Common.DataStructures;
using Infragistics.Win.UltraWinTabbedMdi;
using Infragistics.Win.UltraWinDock;
using FrameworkForge.UI.Interfaces;
using Afca.Common;
using Afca.Common.ServicesInterfaces;

//using DairyCan.UI.SplashScreens;

namespace Afca.UI
{
    public partial class AfcaFrameForm : FrameForm
    {
        private IGenerarService iSvrGenerar;
        private IImportarService iSvrImportar;
        #region Constructor & métodos de inicio
        public AfcaFrameForm()
        {
            iSvrGenerar = AfcaServiceProvider.GetGenerarService();
            iSvrImportar = AfcaServiceProvider.GetImportarService();
           
           

            InitializeComponent();
            dockGroupLeftExplorer = new DockableGroupPane();
            dockGroupLeftExplorer.ChildPaneStyle = ChildPaneStyle.TabGroup;
            dockGroupLeftExplorer.DefaultPaneSettings.TabWidth = -1;

            dockAreaLeft = new DockAreaPane(DockedLocation.DockedLeft);
            dockAreaLeft.Size = new Size(270, 10);
            dockAreaLeft.ChildPaneStyle = ChildPaneStyle.HorizontalSplit;
            dockAreaLeft.Panes.Add(dockGroupLeftExplorer);

            this.ultraDockManager.DockAreas.Add(dockAreaLeft);
            this.ultraDockManager.BeforePaneButtonClick += ultraDockManager_BeforePaneButtonClick;
            this.ultraTabbedMdiManager.MdiParent = this;
        }
        private void AfcaFrameForm_Load(object sender, EventArgs e)
        {
            this.ultraToolbarsManager.Tools["tbbExplorer"].SharedProps.AppearancesSmall.Appearance.Image =
              ResourceProvider.GetIconImage("IconoExplorer");
            this.ultraToolbarsManager.Tools["tbbExplorerDetails"].SharedProps.AppearancesSmall.Appearance.Image =
                ResourceProvider.GetIconImage("IconoExplorerDetails");

            ControlsProvider.ShowAfcaExplorer();

        }
        #endregion

        #region Propiedades
        DockAreaPane dockAreaLeft;
        DockAreaPane dockAreaRight;
        DockAreaPane dockAreaCentral;
        DockableGroupPane dockGroupLeftExplorer;
        DockableGroupPane dockGroupRight;
        DockableGroupPane dockGroupCentral;

        public DockAreaPane DockAreaLeft
        {
            get { return dockAreaLeft; }
            set { dockAreaLeft = value; }
        }
        public DockAreaPane DockAreaRight
        {
            get { return dockAreaRight; }
            set { dockAreaRight = value; }
        }
        public DockAreaPane DockAreaCentral
        {
            get { return dockAreaCentral; }
            set { dockAreaCentral = value; }
        }
        public DockableGroupPane DockGroupLeftExplorer
        {
            get { return dockGroupLeftExplorer; }
            set { dockGroupLeftExplorer = value; }
        }
        #endregion

        #region Métodos dock panels
        public Control AddControlToDockPanel(UserControl controlToDock, DockableGroupPane dockGroupPane, string key, string title, string icon)
        {
            SuspendLayout();

            if (dockGroupPane.ParentDocked.Panes.IndexOf(dockGroupPane) == -1)
            {
                if (dockGroupPane == dockGroupLeftExplorer)
                {
                    dockAreaLeft.Panes.Add(dockGroupPane);
                    dockAreaLeft.Activate();
                }
            }

            controlToDock.Tag = new object[] { dockGroupPane, key };

            if (!dockGroupPane.Panes.Exists(key))
            {
                DockableControlPane paneToDock = new DockableControlPane(
               key
              , title
              , controlToDock);
                paneToDock.Settings.TabAppearance.Image = ResourceProvider.GetIconImage(icon);
                dockGroupPane.Panes.Add(paneToDock);
                dockGroupPane.Activate();
                paneToDock.Show();
                paneToDock.IsSelectedTab = true;
            }
            else
            {
                dockGroupPane.Activate();
                dockGroupPane.Panes[key].Settings.TabAppearance.Image = ResourceProvider.GetIconImage(icon);
                dockGroupPane.Panes[key].Text = title;
                dockGroupPane.Panes[key].Show();
                dockGroupPane.Panes[key].IsSelectedTab = true;
            }

            ResumeLayout();
            this.ultraDockManager.ResumeLayout();
            return controlToDock;
        }
        public void CloseControl(string controlKey, DockableGroupPane dockableGroupPane)
        {
            SuspendLayout();

            if (dockableGroupPane.Panes.Exists(controlKey))
            {
                this.ultraDockManager.BeginUpdate();
                DockableControlPane pane = (DockableControlPane)dockableGroupPane.Panes[controlKey];
                dockableGroupPane.Panes[controlKey].Close(true);
                dockableGroupPane.Panes.Remove(pane);
                pane.Control.Dispose();
                this.ultraDockManager.EndUpdate();
                this.ultraDockManager.ResumeLayout();
            }

            ResumeLayout();
        }

        #region DockManager Events
        private void ultraDockManager_BeforePaneButtonClick(object sender, CancelablePaneButtonEventArgs e)
        {
            if (e.Button == PaneButton.Close)
            {
                DockableGroupPane parentGroup = e.Pane.ParentDocked;
                parentGroup.Panes.Remove(e.Pane.Key);
                this.ultraDockManager.ControlPanes.Remove(e.Pane.Key);
                ((DockableControlPane)e.Pane).Control.Dispose();
            }
        }
        #endregion

        #endregion

        #region Toolbar Manager
        private void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Generar":
                    var et = Afca.Common.Trees.ExplorerTree;
                    var s= sender;
                    Result result = iSvrGenerar.CheckXmlMaestroControlesData(6);
                    if (result.success == false)
                        { DialogResult dr = MessageBox.Show(result.value + "\n" + "No coinciden Maestro y Control", "CHECK Maestro y Control", MessageBoxButtons.YesNo);
                                                if (dr == DialogResult.No) break;
                    }
                   
                    Result resultado= iSvrGenerar.GenerarXMLs(6);
                    break;
                case "Importar":
                   // Result result1 = iSvrImportar.CheckXmlEstablosDuplicadosData(6);
                    Result result1 = iSvrImportar.CheckXmlControlesData(6);
                    if (result1.success == false)
                    {
                        DialogResult dr1 = MessageBox.Show(result1.value + "\n" + "Existen Registros duplicados", "CHECK Controles", MessageBoxButtons.YesNo);
                        if (dr1 == DialogResult.No) break;
                    }
                    Result result3 = iSvrImportar.CheckXmlPartosData(6);
                    if (result3.success == false)
                    {
                        DialogResult dr1 = MessageBox.Show(result3.value + "\n" + "Existen Paratos Duplicados", "CHECK Partos", MessageBoxButtons.YesNo);
                        if (dr1 == DialogResult.No) break;
                    }


                    Result resultado1 = iSvrImportar.ImportarXMLs(6);
                    break;
                case "tbbExplorer":
                case "tbbExplorerDetails":
                    ControlsProvider.ShowAfcaExplorer();
              
                    break;
            }
        }
        #endregion

        private void ultraTabbedMdiManager_InitializeTab(object sender, MdiTabEventArgs e)
        {

        }

        #region variables
        
       
        #endregion
    }
    
}
