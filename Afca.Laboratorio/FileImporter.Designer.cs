using System;

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
      Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileImporter));
      Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
      this.ultraPanelTop = new Infragistics.Win.Misc.UltraPanel();
      this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
      this.TextEstablo = new System.Windows.Forms.TextBox();
      this.CheckFechas = new System.Windows.Forms.CheckBox();
      this.FHasta = new System.Windows.Forms.DateTimePicker();
      this.FDesde = new System.Windows.Forms.DateTimePicker();
      this.API_Medias = new System.Windows.Forms.Button();
      this.API_Industria = new System.Windows.Forms.Button();
      this.botonAPI = new System.Windows.Forms.Button();
      this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
      this.button1 = new System.Windows.Forms.Button();
      this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
      this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
      this.utePath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
      this.utbtnStop = new Infragistics.Win.Misc.UltraButton();
      this.utbtnStart = new Infragistics.Win.Misc.UltraButton();
      this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
      this.uneInterval = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
      this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
      this.ultraPanelDown = new Infragistics.Win.Misc.UltraPanel();
      this.uteLog = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.ultraPanelTop.ClientArea.SuspendLayout();
      this.ultraPanelTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.utePath)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.uneInterval)).BeginInit();
      this.ultraPanelDown.ClientArea.SuspendLayout();
      this.ultraPanelDown.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.uteLog)).BeginInit();
      this.SuspendLayout();
      // 
      // ultraPanelTop
      // 
      // 
      // ultraPanelTop.ClientArea
      // 
      this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel4);
      this.ultraPanelTop.ClientArea.Controls.Add(this.TextEstablo);
      this.ultraPanelTop.ClientArea.Controls.Add(this.CheckFechas);
      this.ultraPanelTop.ClientArea.Controls.Add(this.FHasta);
      this.ultraPanelTop.ClientArea.Controls.Add(this.FDesde);
      this.ultraPanelTop.ClientArea.Controls.Add(this.API_Medias);
      this.ultraPanelTop.ClientArea.Controls.Add(this.API_Industria);
      this.ultraPanelTop.ClientArea.Controls.Add(this.botonAPI);
      this.ultraPanelTop.ClientArea.Controls.Add(this.ultraButton2);
      this.ultraPanelTop.ClientArea.Controls.Add(this.button1);
      this.ultraPanelTop.ClientArea.Controls.Add(this.ultraButton1);
      this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel3);
      this.ultraPanelTop.ClientArea.Controls.Add(this.utePath);
      this.ultraPanelTop.ClientArea.Controls.Add(this.utbtnStop);
      this.ultraPanelTop.ClientArea.Controls.Add(this.utbtnStart);
      this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel2);
      this.ultraPanelTop.ClientArea.Controls.Add(this.uneInterval);
      this.ultraPanelTop.ClientArea.Controls.Add(this.ultraLabel1);
      this.ultraPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.ultraPanelTop.Location = new System.Drawing.Point(0, 0);
      this.ultraPanelTop.Margin = new System.Windows.Forms.Padding(4);
      this.ultraPanelTop.Name = "ultraPanelTop";
      this.ultraPanelTop.Size = new System.Drawing.Size(612, 393);
      this.ultraPanelTop.TabIndex = 0;
      // 
      // ultraLabel4
      // 
      appearance1.TextVAlignAsString = "Middle";
      this.ultraLabel4.Appearance = appearance1;
      this.ultraLabel4.Location = new System.Drawing.Point(192, 287);
      this.ultraLabel4.Margin = new System.Windows.Forms.Padding(4);
      this.ultraLabel4.Name = "ultraLabel4";
      this.ultraLabel4.Size = new System.Drawing.Size(155, 28);
      this.ultraLabel4.TabIndex = 16;
      this.ultraLabel4.Text = "Establo (J39xxxx)";
     
      // 
      // TextEstablo
      // 
      this.TextEstablo.Location = new System.Drawing.Point(44, 287);
      this.TextEstablo.Margin = new System.Windows.Forms.Padding(4);
      this.TextEstablo.MaxLength = 7;
      this.TextEstablo.Name = "TextEstablo";
      this.TextEstablo.Size = new System.Drawing.Size(139, 22);
      this.TextEstablo.TabIndex = 15;
      
      // 
      // CheckFechas
      // 
      this.CheckFechas.AutoSize = true;
      this.CheckFechas.Location = new System.Drawing.Point(184, 319);
      this.CheckFechas.Margin = new System.Windows.Forms.Padding(4);
      this.CheckFechas.Name = "CheckFechas";
      this.CheckFechas.Size = new System.Drawing.Size(106, 20);
      this.CheckFechas.TabIndex = 14;
      this.CheckFechas.Text = "Usar Fechas";
      this.CheckFechas.UseVisualStyleBackColor = true;
    
      // 
      // FHasta
      // 
      this.FHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.FHasta.Location = new System.Drawing.Point(44, 351);
      this.FHasta.Margin = new System.Windows.Forms.Padding(4);
      this.FHasta.Name = "FHasta";
      this.FHasta.Size = new System.Drawing.Size(131, 22);
      this.FHasta.TabIndex = 13;
      // 
      // FDesde
      // 
      this.FDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.FDesde.Location = new System.Drawing.Point(44, 319);
      this.FDesde.Margin = new System.Windows.Forms.Padding(4);
      this.FDesde.Name = "FDesde";
      this.FDesde.Size = new System.Drawing.Size(131, 22);
      this.FDesde.TabIndex = 12;
      // 
      // API_Medias
      // 
      this.API_Medias.Image = ((System.Drawing.Image)(resources.GetObject("API_Medias.Image")));
      this.API_Medias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.API_Medias.Location = new System.Drawing.Point(400, 319);
      this.API_Medias.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.API_Medias.Name = "API_Medias";
      this.API_Medias.Size = new System.Drawing.Size(197, 48);
      this.API_Medias.TabIndex = 11;
      this.API_Medias.Text = "API Media Industria";
      this.API_Medias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.API_Medias.UseVisualStyleBackColor = true;
      this.API_Medias.Click += new System.EventHandler(this.API_Medias_Click);
      // 
      // API_Industria
      // 
      this.API_Industria.Image = ((System.Drawing.Image)(resources.GetObject("API_Industria.Image")));
      this.API_Industria.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.API_Industria.Location = new System.Drawing.Point(403, 246);
      this.API_Industria.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.API_Industria.Name = "API_Industria";
      this.API_Industria.Size = new System.Drawing.Size(197, 48);
      this.API_Industria.TabIndex = 10;
      this.API_Industria.Text = "API Tanque Industria";
      this.API_Industria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.API_Industria.UseVisualStyleBackColor = true;
      this.API_Industria.Click += new System.EventHandler(this.API_Industria_Click);
      // 
      // botonAPI
      // 
      this.botonAPI.Image = ((System.Drawing.Image)(resources.GetObject("botonAPI.Image")));
      this.botonAPI.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.botonAPI.Location = new System.Drawing.Point(403, 183);
      this.botonAPI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.botonAPI.Name = "botonAPI";
      this.botonAPI.Size = new System.Drawing.Size(197, 48);
      this.botonAPI.TabIndex = 9;
      this.botonAPI.Text = "API Control Lechero";
      this.botonAPI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.botonAPI.UseVisualStyleBackColor = true;
      this.botonAPI.Click += new System.EventHandler(this.botonAPI_Click);
      // 
      // ultraButton2
      // 
      appearance2.Image = global::Afca.Laboratorio.Properties.Resources.bullet_triangle_green;
      this.ultraButton2.Appearance = appearance2;
      this.ultraButton2.ImageSize = new System.Drawing.Size(32, 32);
      this.ultraButton2.Location = new System.Drawing.Point(437, 78);
      this.ultraButton2.Margin = new System.Windows.Forms.Padding(4);
      this.ultraButton2.Name = "ultraButton2";
      this.ultraButton2.Size = new System.Drawing.Size(152, 42);
      this.ultraButton2.TabIndex = 8;
      this.ultraButton2.Text = "Tanque manual";
      this.ultraButton2.Click += new System.EventHandler(this.ultraButton2_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(545, 34);
      this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(55, 30);
      this.button1.TabIndex = 7;
      this.button1.Text = "Fich.";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // ultraButton1
      // 
      appearance3.Image = global::Afca.Laboratorio.Properties.Resources.bullet_triangle_green;
      this.ultraButton1.Appearance = appearance3;
      this.ultraButton1.ImageSize = new System.Drawing.Size(32, 32);
      this.ultraButton1.Location = new System.Drawing.Point(437, 135);
      this.ultraButton1.Margin = new System.Windows.Forms.Padding(4);
      this.ultraButton1.Name = "ultraButton1";
      this.ultraButton1.Size = new System.Drawing.Size(129, 42);
      this.ultraButton1.TabIndex = 6;
      this.ultraButton1.Text = "Start manual";
      this.ultraButton1.Click += new System.EventHandler(this.ultraButton1_Click);
      // 
      // ultraLabel3
      // 
      appearance4.TextVAlignAsString = "Middle";
      this.ultraLabel3.Appearance = appearance4;
      this.ultraLabel3.Location = new System.Drawing.Point(367, 73);
      this.ultraLabel3.Margin = new System.Windows.Forms.Padding(4);
      this.ultraLabel3.Name = "ultraLabel3";
      this.ultraLabel3.Size = new System.Drawing.Size(85, 28);
      this.ultraLabel3.TabIndex = 5;
      this.ultraLabel3.Text = "minutos";
      // 
      // utePath
      // 
      this.utePath.Location = new System.Drawing.Point(69, 39);
      this.utePath.Margin = new System.Windows.Forms.Padding(4);
      this.utePath.Name = "utePath";
      this.utePath.Size = new System.Drawing.Size(469, 24);
      this.utePath.TabIndex = 4;
      this.utePath.Text = "C:\\AFCA\\Contlech\\CL130623_228.txt";
      // 
      // utbtnStop
      // 
      appearance5.Image = global::Afca.Laboratorio.Properties.Resources.bullet_square_red;
      this.utbtnStop.Appearance = appearance5;
      this.utbtnStop.ImageSize = new System.Drawing.Size(32, 32);
      this.utbtnStop.Location = new System.Drawing.Point(184, 135);
      this.utbtnStop.Margin = new System.Windows.Forms.Padding(4);
      this.utbtnStop.Name = "utbtnStop";
      this.utbtnStop.Size = new System.Drawing.Size(105, 42);
      this.utbtnStop.TabIndex = 3;
      this.utbtnStop.Text = "Stop";
      this.utbtnStop.Click += new System.EventHandler(this.utbtnStop_Click);
      // 
      // utbtnStart
      // 
      appearance6.Image = global::Afca.Laboratorio.Properties.Resources.bullet_triangle_green;
      this.utbtnStart.Appearance = appearance6;
      this.utbtnStart.ImageSize = new System.Drawing.Size(32, 32);
      this.utbtnStart.Location = new System.Drawing.Point(76, 135);
      this.utbtnStart.Margin = new System.Windows.Forms.Padding(4);
      this.utbtnStart.Name = "utbtnStart";
      this.utbtnStart.Size = new System.Drawing.Size(100, 42);
      this.utbtnStart.TabIndex = 2;
      this.utbtnStart.Text = "Start";
      this.utbtnStart.Click += new System.EventHandler(this.utbtnStart_Click);
      // 
      // ultraLabel2
      // 
      this.ultraLabel2.Location = new System.Drawing.Point(69, 15);
      this.ultraLabel2.Margin = new System.Windows.Forms.Padding(4);
      this.ultraLabel2.Name = "ultraLabel2";
      this.ultraLabel2.Size = new System.Drawing.Size(259, 28);
      this.ultraLabel2.TabIndex = 0;
      this.ultraLabel2.Text = "Ruta de los ficheros de laboratorio";
      // 
      // uneInterval
      // 
      this.uneInterval.Location = new System.Drawing.Point(236, 73);
      this.uneInterval.Margin = new System.Windows.Forms.Padding(4);
      this.uneInterval.Name = "uneInterval";
      this.uneInterval.Size = new System.Drawing.Size(123, 24);
      this.uneInterval.TabIndex = 1;
      this.uneInterval.ValueChanged += new System.EventHandler(this.uneInterval_ValueChanged);
      // 
      // ultraLabel1
      // 
      this.ultraLabel1.Location = new System.Drawing.Point(69, 78);
      this.ultraLabel1.Margin = new System.Windows.Forms.Padding(4);
      this.ultraLabel1.Name = "ultraLabel1";
      this.ultraLabel1.Size = new System.Drawing.Size(173, 28);
      this.ultraLabel1.TabIndex = 0;
      this.ultraLabel1.Text = "Importar ficheros cada ";
      // 
      // ultraPanelDown
      // 
      // 
      // ultraPanelDown.ClientArea
      // 
      this.ultraPanelDown.ClientArea.Controls.Add(this.uteLog);
      this.ultraPanelDown.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ultraPanelDown.Location = new System.Drawing.Point(0, 393);
      this.ultraPanelDown.Margin = new System.Windows.Forms.Padding(4);
      this.ultraPanelDown.Name = "ultraPanelDown";
      this.ultraPanelDown.Size = new System.Drawing.Size(612, 251);
      this.ultraPanelDown.TabIndex = 1;
      // 
      // uteLog
      // 
      this.uteLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.uteLog.Location = new System.Drawing.Point(0, 0);
      this.uteLog.Margin = new System.Windows.Forms.Padding(4);
      this.uteLog.Multiline = true;
      this.uteLog.Name = "uteLog";
      this.uteLog.Scrollbars = System.Windows.Forms.ScrollBars.Both;
      this.uteLog.Size = new System.Drawing.Size(612, 251);
      this.uteLog.TabIndex = 0;
      this.uteLog.ValueChanged += new System.EventHandler(this.uteLog_ValueChanged);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // FileImporter
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(612, 644);
      this.Controls.Add(this.ultraPanelDown);
      this.Controls.Add(this.ultraPanelTop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4);
      this.MaximizeBox = false;
      this.Name = "FileImporter";
      this.Text = "Proceso importación ficheros laboratorio";
      this.ultraPanelTop.ClientArea.ResumeLayout(false);
      this.ultraPanelTop.ClientArea.PerformLayout();
      this.ultraPanelTop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.utePath)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.uneInterval)).EndInit();
      this.ultraPanelDown.ClientArea.ResumeLayout(false);
      this.ultraPanelDown.ClientArea.PerformLayout();
      this.ultraPanelDown.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.uteLog)).EndInit();
      this.ResumeLayout(false);

        }

    private void uneInterval_ValueChanged(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void uteLog_ValueChanged(object sender, EventArgs e)
    {
      //throw new NotImplementedException();
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
        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Infragistics.Win.Misc.UltraButton ultraButton2;
    private System.Windows.Forms.Button botonAPI;
    private System.Windows.Forms.Button API_Industria;
    private System.Windows.Forms.DateTimePicker FDesde;
    private System.Windows.Forms.DateTimePicker FHasta;
    private System.Windows.Forms.Button API_Medias;
    private System.Windows.Forms.CheckBox CheckFechas;
    private Infragistics.Win.Misc.UltraLabel ultraLabel4;
    private System.Windows.Forms.TextBox TextEstablo;
  }
}