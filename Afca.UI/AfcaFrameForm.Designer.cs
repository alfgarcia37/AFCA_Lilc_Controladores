namespace Afca.UI
{
    partial class AfcaFrameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("toolBarPrincipal");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("tbbExplorer");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("tbbExplorerDetails");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Generar");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Importar", "");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("tbbExplorer");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("tbbExplorerDetails");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Generar");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Importar", "");
            this.appStylistRuntime = new Infragistics.Win.AppStyling.Runtime.AppStylistRuntime(this.components);
            this.ultraTabbedMdiManager = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraDockManager
            // 
            this.ultraDockManager.DefaultGroupSettings.CloseButtonBehavior = Infragistics.Win.UltraWinDock.CloseButtonBehavior.CloseActiveTab;
            this.ultraDockManager.DefaultGroupSettings.TabSizing = Infragistics.Win.UltraWinTabs.TabSizing.AutoSize;
            this.ultraDockManager.DefaultGroupSettings.TabStyle = Infragistics.Win.UltraWinTabs.TabStyle.VisualStudio;
            this.ultraDockManager.DefaultPaneSettings.ForceSerialization = true;
            // 
            // _FrameFormUnpinnedTabAreaBottom
            // 
            this._FrameFormUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 615);
            this._FrameFormUnpinnedTabAreaBottom.Size = new System.Drawing.Size(1008, 0);
            // 
            // _FrameFormUnpinnedTabAreaTop
            // 
            this._FrameFormUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 27);
            this._FrameFormUnpinnedTabAreaTop.Size = new System.Drawing.Size(1008, 0);
            // 
            // _FrameFormUnpinnedTabAreaRight
            // 
            this._FrameFormUnpinnedTabAreaRight.Location = new System.Drawing.Point(1008, 27);
            this._FrameFormUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 588);
            // 
            // _FrameFormUnpinnedTabAreaLeft
            // 
            this._FrameFormUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 27);
            this._FrameFormUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 588);
            // 
            // ultraToolbarsManager
            // 
            this.ultraToolbarsManager.MenuSettings.ForceSerialization = true;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool3,
            buttonTool4,
            buttonTool5,
            stateButtonTool1});
            ultraToolbar1.Text = "Barra principal";
            this.ultraToolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            this.ultraToolbarsManager.ToolbarSettings.ToolOrientation = Infragistics.Win.UltraWinToolbars.ToolOrientation.Horizontal;
            buttonTool1.SharedPropsInternal.Caption = "Ver explorador";
            buttonTool2.SharedPropsInternal.Caption = "Ver detalles del explorador";
            buttonTool6.SharedPropsInternal.Caption = "Generar";
            buttonTool6.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            stateButtonTool2.SharedPropsInternal.Caption = "Importar";
            stateButtonTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            this.ultraToolbarsManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1,
            buttonTool2,
            buttonTool6,
            stateButtonTool2});
            this.ultraToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager_ToolClick);
            // 
            // ultraTabbedMdiManager
            // 
            this.ultraTabbedMdiManager.MdiParent = this;
            this.ultraTabbedMdiManager.TabSettings.AllowClose = Infragistics.Win.DefaultableBoolean.True;
            this.ultraTabbedMdiManager.TabSettings.DisplayFormIcon = Infragistics.Win.DefaultableBoolean.True;
            this.ultraTabbedMdiManager.InitializeTab += new Infragistics.Win.UltraWinTabbedMdi.MdiTabEventHandler(this.ultraTabbedMdiManager_InitializeTab);
            // 
            // AfcaFrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 638);
            this.Name = "AfcaFrameForm";
            this.Text = "AFCA";
            this.Load += new System.EventHandler(this.AfcaFrameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.AppStyling.Runtime.AppStylistRuntime appStylistRuntime;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiManager;
    }
}