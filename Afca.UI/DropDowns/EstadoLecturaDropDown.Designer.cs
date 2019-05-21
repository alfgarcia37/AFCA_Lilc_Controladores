namespace Afca.UI.DropDowns
{
    partial class EstadoLecturaDropDown
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("EstadoLectura", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EstadoID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Nombre");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Observaciones");
            this.estadoLecturaData = new Afca.Common.DataSets.EstadoLecturaData();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDropDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.estadoLecturaData)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraDropDown
            // 
            this.ultraDropDown.DataMember = "EstadoLectura";
            this.ultraDropDown.DataSource = this.estadoLecturaData;
            ultraGridColumn1.Header.Editor = null;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "Estado";
            ultraGridColumn2.Header.Editor = null;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 165;
            ultraGridColumn3.Header.Editor = null;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.ultraDropDown.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraDropDown.DisplayMember = "Nombre";
            this.ultraDropDown.Size = new System.Drawing.Size(176, 145);
            this.ultraDropDown.ValueMember = "EstadoID";
            // 
            // estadoLecturaData
            // 
            this.estadoLecturaData.DataSetName = "EstadoLecturaData";
            this.estadoLecturaData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // EstadoLecturaDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "EstadoLecturaDropDown";
            this.Size = new System.Drawing.Size(176, 145);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDropDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.estadoLecturaData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.DataSets.EstadoLecturaData estadoLecturaData;
    }
}
