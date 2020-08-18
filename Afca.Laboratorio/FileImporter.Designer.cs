namespace Afca.Laboratorio
{
    partial class FileImporter
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileImporter));
            this.ultraPanelTop = new Infragistics.Win.Misc.UltraPanel();
            this.ultraPanelDown = new Infragistics.Win.Misc.UltraPanel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.uneInterval = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.utbtnStart = new Infragistics.Win.Misc.UltraButton();
            this.utbtnStop = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.utePath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.uteLog = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraPanelTop.ClientArea.SuspendLayout();
            this.ultraPanelTop.SuspendLayout();
            this.ultraPanelDown.ClientArea.SuspendLayout();
            this.ultraPanelDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uneInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uteLog)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraPanelTop
            // 
            // 
            // ultraPanelTop.ClientArea
            // 
            this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel3);
            this.ultraPanelTop.ClientArea.Controls.Add(this.utePath);
            this.ultraPanelTop.ClientArea.Controls.Add(this.utbtnStop);
            this.ultraPanelTop.ClientArea.Controls.Add(this.utbtnStart);
            this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel2);
            this.ultraPanelTop.ClientArea.Controls.Add(this.uneInterval);
            this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel1);
            this.ultraPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPanelTop.Location = new System.Drawing.Point(0, 0);
            this.ultraPanelTop.Name = "ultraPanelTop";
            this.ultraPanelTop.Size = new System.Drawing.Size(452, 164);
            this.ultraPanelTop.TabIndex = 0;
            // 
            // ultraPanelDown
            // 
            // 
            // ultraPanelDown.ClientArea
            // 
            this.ultraPanelDown.ClientArea.Controls.Add(this.uteLog);
            this.ultraPanelDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanelDown.Location = new System.Drawing.Point(0, 164);
            this.ultraPanelDown.Name = "ultraPanelDown";
            this.ultraPanelDown.Size = new System.Drawing.Size(452, 286);
            this.ultraPanelDown.TabIndex = 1;
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(52, 63);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(130, 23);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Importar ficheros cada ";
            // 
            // uneInterval
            // 
            this.uneInterval.Location = new System.Drawing.Point(177, 59);
            this.uneInterval.Name = "uneInterval";
            this.uneInterval.Size = new System.Drawing.Size(92, 21);
            this.uneInterval.TabIndex = 1;
            // 
            // utbtnStart
            // 
            appearance3.Image = global::Afca.Laboratorio.Properties.Resources.bullet_triangle_green;
            this.utbtnStart.Appearance = appearance3;
            this.utbtnStart.ImageSize = new System.Drawing.Size(32, 32);
            this.utbtnStart.Location = new System.Drawing.Point(57, 110);
            this.utbtnStart.Name = "utbtnStart";
            this.utbtnStart.Size = new System.Drawing.Size(75, 34);
            this.utbtnStart.TabIndex = 2;
            this.utbtnStart.Text = "Start";
            this.utbtnStart.Click += new System.EventHandler(this.utbtnStart_Click);
            // 
            // utbtnStop
            // 
            appearance2.Image = global::Afca.Laboratorio.Properties.Resources.bullet_square_red;
            this.utbtnStop.Appearance = appearance2;
            this.utbtnStop.ImageSize = new System.Drawing.Size(32, 32);
            this.utbtnStop.Location = new System.Drawing.Point(138, 110);
            this.utbtnStop.Name = "utbtnStop";
            this.utbtnStop.Size = new System.Drawing.Size(79, 34);
            this.utbtnStop.TabIndex = 3;
            this.utbtnStop.Text = "Stop";
            this.utbtnStop.Click += new System.EventHandler(this.utbtnStop_Click);
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(52, 12);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(194, 23);
            this.ultraLabel2.TabIndex = 0;
            this.ultraLabel2.Text = "Ruta de los ficheros de laboratorio";
            // 
            // utePath
            // 
            this.utePath.Location = new System.Drawing.Point(52, 32);
            this.utePath.Name = "utePath";
            this.utePath.Size = new System.Drawing.Size(352, 21);
            this.utePath.TabIndex = 4;
            // 
            // ultraLabel3
            // 
            appearance1.TextVAlignAsString = "Middle";
            this.ultraLabel3.Appearance = appearance1;
            this.ultraLabel3.Location = new System.Drawing.Point(275, 59);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(64, 23);
            this.ultraLabel3.TabIndex = 5;
            this.ultraLabel3.Text = "minutos";
            // 
            // uteLog
            // 
            this.uteLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uteLog.Location = new System.Drawing.Point(0, 0);
            this.uteLog.Multiline = true;
            this.uteLog.Name = "uteLog";
            this.uteLog.Size = new System.Drawing.Size(452, 286);
            this.uteLog.TabIndex = 0;
            // 
            // FileImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 450);
            this.Controls.Add(this.ultraPanelDown);
            this.Controls.Add(this.ultraPanelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FileImporter";
            this.Text = "Proceso importación ficheros labortorio";
            this.ultraPanelTop.ClientArea.ResumeLayout(false);
            this.ultraPanelTop.ClientArea.PerformLayout();
            this.ultraPanelTop.ResumeLayout(false);
            this.ultraPanelDown.ClientArea.ResumeLayout(false);
            this.ultraPanelDown.ClientArea.PerformLayout();
            this.ultraPanelDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uneInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uteLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanelTop;
        private Infragistics.Win.Misc.UltraPanel ultraPanelDown;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor utePath;
        private Infragistics.Win.Misc.UltraButton utbtnStop;
        private Infragistics.Win.Misc.UltraButton utbtnStart;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneInterval;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor uteLog;
    }
}