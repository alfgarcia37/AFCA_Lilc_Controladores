
namespace Afca.UI.Dialogs
{
    partial class DialogGanaderiaForm
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
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.ultraPanelBpdy = new Infragistics.Win.Misc.UltraPanel();
            this.uteGanaderia = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraPanelButtons = new Infragistics.Win.Misc.UltraPanel();
            this.utbtnCancelar = new Infragistics.Win.Misc.UltraButton();
            this.utbtnAceptar = new Infragistics.Win.Misc.UltraButton();
            this.uchkTodas = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraPanelBpdy.ClientArea.SuspendLayout();
            this.ultraPanelBpdy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uteGanaderia)).BeginInit();
            this.ultraPanelButtons.ClientArea.SuspendLayout();
            this.ultraPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uchkTodas)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraPanelBpdy
            // 
            // 
            // ultraPanelBpdy.ClientArea
            // 
            this.ultraPanelBpdy.ClientArea.Controls.Add(this.uchkTodas);
            this.ultraPanelBpdy.ClientArea.Controls.Add(this.ultraPanelButtons);
            this.ultraPanelBpdy.ClientArea.Controls.Add(this.uteGanaderia);
            this.ultraPanelBpdy.ClientArea.Controls.Add(this.ultraLabel1);
            this.ultraPanelBpdy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanelBpdy.Location = new System.Drawing.Point(0, 0);
            this.ultraPanelBpdy.Name = "ultraPanelBpdy";
            this.ultraPanelBpdy.Size = new System.Drawing.Size(386, 189);
            this.ultraPanelBpdy.TabIndex = 1;
            // 
            // uteGanaderia
            // 
            this.uteGanaderia.Enabled = false;
            this.uteGanaderia.Location = new System.Drawing.Point(33, 89);
            this.uteGanaderia.Name = "uteGanaderia";
            this.uteGanaderia.Size = new System.Drawing.Size(321, 21);
            this.uteGanaderia.TabIndex = 1;
            // 
            // ultraLabel1
            // 
            appearance2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ultraLabel1.Appearance = appearance2;
            this.ultraLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel1.Location = new System.Drawing.Point(33, 60);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(321, 23);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Introduzca el Código de la GANADERIA a generar";
            // 
            // ultraPanelButtons
            // 
            // 
            // ultraPanelButtons.ClientArea
            // 
            this.ultraPanelButtons.ClientArea.Controls.Add(this.utbtnCancelar);
            this.ultraPanelButtons.ClientArea.Controls.Add(this.utbtnAceptar);
            this.ultraPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraPanelButtons.Location = new System.Drawing.Point(0, 133);
            this.ultraPanelButtons.Name = "ultraPanelButtons";
            this.ultraPanelButtons.Size = new System.Drawing.Size(386, 56);
            this.ultraPanelButtons.TabIndex = 2;
            // 
            // utbtnCancelar
            // 
            this.utbtnCancelar.Location = new System.Drawing.Point(279, 14);
            this.utbtnCancelar.Name = "utbtnCancelar";
            this.utbtnCancelar.Size = new System.Drawing.Size(75, 23);
            this.utbtnCancelar.TabIndex = 1;
            this.utbtnCancelar.Text = "Cancelar";
            this.utbtnCancelar.Click += new System.EventHandler(this.utbtnCancelar_Click);
            // 
            // utbtnAceptar
            // 
            this.utbtnAceptar.Location = new System.Drawing.Point(198, 14);
            this.utbtnAceptar.Name = "utbtnAceptar";
            this.utbtnAceptar.Size = new System.Drawing.Size(75, 23);
            this.utbtnAceptar.TabIndex = 0;
            this.utbtnAceptar.Text = "Aceptar";
            this.utbtnAceptar.Click += new System.EventHandler(this.utbtnAceptar_Click);
            // 
            // uchkTodas
            // 
            this.uchkTodas.Checked = true;
            this.uchkTodas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uchkTodas.Location = new System.Drawing.Point(33, 21);
            this.uchkTodas.Name = "uchkTodas";
            this.uchkTodas.Size = new System.Drawing.Size(120, 20);
            this.uchkTodas.TabIndex = 3;
            this.uchkTodas.Text = "TODAS";
            this.uchkTodas.CheckedChanged += new System.EventHandler(this.uchkTodas_CheckedChanged);
            // 
            // DialogGanaderiaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 189);
            this.Controls.Add(this.ultraPanelBpdy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogGanaderiaForm";
            this.Text = "Ganaderia";
            this.ultraPanelBpdy.ClientArea.ResumeLayout(false);
            this.ultraPanelBpdy.ClientArea.PerformLayout();
            this.ultraPanelBpdy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uteGanaderia)).EndInit();
            this.ultraPanelButtons.ClientArea.ResumeLayout(false);
            this.ultraPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uchkTodas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanelBpdy;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor uteGanaderia;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor uchkTodas;
        private Infragistics.Win.Misc.UltraPanel ultraPanelButtons;
        private Infragistics.Win.Misc.UltraButton utbtnCancelar;
        private Infragistics.Win.Misc.UltraButton utbtnAceptar;
    }
}