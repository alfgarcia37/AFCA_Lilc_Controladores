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
    public partial class DialogRemesaForm : Form
    {
        public string RemesaCodigoSelected
        {
            get
            {
                return DataOperations.cls(uteRemesa.Text, "").ToString();
            }
        }
        public DialogRemesaForm()
        {
            InitializeComponent();
        }

        private void utbtnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(uteRemesa.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                DialogsManager.ShowErrorDialog("Aviso"
                    ,"Debe introducir un código de remesa");
            }
        }

        private void utbtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
