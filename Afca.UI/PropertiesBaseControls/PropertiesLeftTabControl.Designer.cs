namespace Afca.UI.PropertiesBaseControls
{
    partial class PropertiesLeftTabControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraToolbarsManager
            // 
            this.ultraToolbarsManager.MenuSettings.ForceSerialization = true;
            this.ultraToolbarsManager.ToolbarSettings.ForceSerialization = true;
            // 
            // ultraTabControl
            // 
            this.ultraTabControl.InterTabSpacing = new Infragistics.Win.DefaultableInteger(1);
            this.ultraTabControl.MaxVisibleTabRows = 10;
            this.ultraTabControl.MinTabWidth = 30;
            this.ultraTabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPageSelected;
            appearance1.BackColor = System.Drawing.Color.LightGray;
            appearance1.BackColor2 = System.Drawing.Color.DarkGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraTabControl.TabHeaderAreaAppearance = appearance1;
            this.ultraTabControl.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
            this.ultraTabControl.TabPadding = new System.Drawing.Size(10, 1);
            this.ultraTabControl.TabPageMargins.Bottom = 20;
            this.ultraTabControl.TabPageMargins.Left = 20;
            this.ultraTabControl.TabPageMargins.Right = 20;
            this.ultraTabControl.TabPageMargins.Top = 20;
            this.ultraTabControl.TextOrientation = Infragistics.Win.UltraWinTabs.TextOrientation.Horizontal;
            // 
            // PropertiesLeftTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "PropertiesLeftTabControl";
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
