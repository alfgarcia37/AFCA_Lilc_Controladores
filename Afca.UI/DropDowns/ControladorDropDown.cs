using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrameworkForge.UI;
using FrameworkForge.Common.Interfaces;
using Afca.Common;
using Afca.Common.ServicesInterfaces;


namespace Afca.UI.DropDowns
{
    public partial class ControladorDropDown : DropDownControl
    {
        public ControladorDropDown()
        {
            InitializeComponent();
            InitializeService();
            LoadDropDownData();
        }
        public override void InitializeService()
        {
            Service = AfcaServiceProvider.GetControladorService();
        }
        public override void LoadDropDownData()
        {

            controladorData = ((IControladorService)this.Service).LoadControladorData();
            ultraDropDown.DataSource = controladorData;
            ultraDropDown.DataMember = controladorData.Controlador.TableName;
            ultraDropDown.DisplayMember = "NombreCompuesto";
            ultraDropDown.ValueMember = "Id";
        }
    }
}
