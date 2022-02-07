using FrameworkForge.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afca.UI.Dialogs
{
    public partial class DialogGanaderiaForm : Form
    {
        public string GanaderiaCodigoSelected
        {
            get
            {
                return uchkTodas.Checked ? "*" :
                    DataOperations.cls(uteGanaderia.Text, "*").ToString();
            }
        }
        public DialogGanaderiaForm()
        {
            InitializeComponent();
        }

        private void utbtnAceptar_Click(object sender, EventArgs e)
        {
            if (uchkTodas.Checked ||
                (!uchkTodas.Checked && !String.IsNullOrEmpty(uteGanaderia.Text)))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                DialogsManager.ShowErrorDialog("Aviso"
                    , "Debe introducir un código de ganaderia or marcar la opción todas");
            }
        }

        private void utbtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void uchkTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (uchkTodas.Checked)
            {
                uteGanaderia.Text = "";
                uteGanaderia.Enabled = false;
            }
            else
            {
                uteGanaderia.Enabled = true;
            }
        }
    }
}
