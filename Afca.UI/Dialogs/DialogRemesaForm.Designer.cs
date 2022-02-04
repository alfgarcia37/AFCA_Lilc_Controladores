
namespace Afca.UI.Dialogs
{
    partial class DialogRemesaForm
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
            this.ultraPanelButtons = new Infragistics.Win.Misc.UltraPanel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.uteRemesa = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.utbtnAceptar = new Infragistics.Win.Misc.UltraButton();
            this.utbtnCancelar = new Infragistics.Win.Misc.UltraButton();
            this.ultraPanelBpdy.ClientArea.SuspendLayout();
            this.ultraPanelBpdy.SuspendLayout();
            this.ultraPanelButtons.ClientArea.SuspendLayout();
            this.ultraPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uteRemesa)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraPanelBpdy
            // 
            // 
            // ultraPanelBpdy.ClientArea
            // 
            this.ultraPanelBpdy.ClientArea.Controls.Add(this.uteRemesa);
            this.ultraPanelBpdy.ClientArea.Controls.Add(this.ultraLabel1);
            this.ultraPanelBpdy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanelBpdy.Location = new System.Drawing.Point(0, 0);
            this.ultraPanelBpdy.Name = "ultraPanelBpdy";
            this.ultraPanelBpdy.Size = new System.Drawing.Size(388, 158);
            this.ultraPanelBpdy.TabIndex = 0;
            // 
            // ultraPanelButtons
            // 
            // 
            // ultraPanelButtons.ClientArea
            // 
            this.ultraPanelButtons.ClientArea.Controls.Add(this.utbtnCancelar);
            this.ultraPanelButtons.ClientArea.Controls.Add(this.utbtnAceptar);
            this.ultraPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraPanelButtons.Location = new System.Drawing.Point(0, 102);
            this.ultraPanelButtons.Name = "ultraPanelButtons";
            this.ultraPanelButtons.Size = new System.Drawing.Size(388, 56);
            this.ultraPanelButtons.TabIndex = 1;
            // 
            // ultraLabel1
            // 
            appearance2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ultraLabel1.Appearance = appearance2;
            this.ultraLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel1.Location = new System.Drawing.Point(33, 28);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(321, 23);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Introduzca el Código de REMESA a generar";
            // 
            // uteRemesa
            // 
            this.uteRemesa.Location = new System.Drawing.Point(33, 57);
            this.uteRemesa.Name = "uteRemesa";
            this.uteRemesa.Size = new System.Drawing.Size(321, 21);
            this.uteRemesa.TabIndex = 1;
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
            // utbtnCancelar
            // 
            this.utbtnCancelar.Location = new System.Drawing.Point(279, 14);
            this.utbtnCancelar.Name = "utbtnCancelar";
            this.utbtnCancelar.Size = new System.Drawing.Size(75, 23);
            this.utbtnCancelar.TabIndex = 1;
            this.utbtnCancelar.Text = "Cancelar";
            this.utbtnCancelar.Click += new System.EventHandler(this.utbtnCancelar_Click);
            // 
            // DialogRemesaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 158);
            this.Controls.Add(this.ultraPanelButtons);
            this.Controls.Add(this.ultraPanelBpdy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogRemesaForm";
            this.Text = "Remesa";
            this.ultraPanelBpdy.ClientArea.ResumeLayout(false);
            this.ultraPanelBpdy.ClientArea.PerformLayout();
            this.ultraPanelBpdy.ResumeLayout(false);
            this.ultraPanelButtons.ClientArea.ResumeLayout(false);
            this.ultraPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uteRemesa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanelBpdy;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor uteRemesa;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraPanel ultraPanelButtons;
        private Infragistics.Win.Misc.UltraButton utbtnCancelar;
        private Infragistics.Win.Misc.UltraButton utbtnAceptar;
    }
}