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
    public partial class EstadoLecturaDropDown : DropDownControl
    {
        public EstadoLecturaDropDown()
        {
            InitializeComponent();
            InitializeService();
            LoadDropDownData();
        }
        public override void InitializeService()
        {
            Service = AfcaServiceProvider.GetEstadoLecturaService();
        }
        public override void LoadDropDownData()
        {

            estadoLecturaData = ((IEstadoLecturaService)this.Service).LoadEstadoLecturaData();
            ultraDropDown.DataSource = estadoLecturaData;
            ultraDropDown.DataMember = estadoLecturaData.EstadoLectura.TableName;
            ultraDropDown.DisplayMember = "Nombre";
            ultraDropDown.ValueMember = "EstadoID";
        }
    }
}
