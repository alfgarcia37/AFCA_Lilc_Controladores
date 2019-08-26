using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using FrameworkForge.UI;
using FrameworkForge.UI.Providers;
using FrameworkForge.Common;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Afca.Common;
using Afca.Common.ServicesInterfaces;
using Afca.Common.DataSets;
using FrameworkForge.Common.DataStructures;

namespace Afca.UI
{
    public partial class LecturaControlDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        int? codigoControlador; 

        public LecturaControlDataGridControl()
        {
            InitializeComponent();
            InitializeDropDowns();
            LoadData();
        }
        public LecturaControlDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            InitializeDropDowns();           
            LoadData();
        }
        public LecturaControlDataGridControl(ItemData argItemContainer,int argCodigoControlador)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            codigoControlador = argCodigoControlador;
            InitializeDropDowns();
            LoadData();
        }
        protected override void InitializeAppearance()
        {
            base.InitializeAppearance();

            ControlsThemeBuilder.GridMakeUp(this.ultraGridData,ThemeColor.Verde);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0],ThemeColor.Verde);
            SetDataGridFilters();

            if (codigoControlador.HasValue)
            {
                if(Parent!=null && Parent is Form &&
                    itemContainer!=null && itemContainer.ItemColumns.ContainsKey("Nombre"))
                {
                    ((Form)Parent).Text = itemContainer.ItemColumns["Nombre"].Data.Value.ToString();
                    ultraGridData.DisplayLayout.Bands[0].Columns["NumControler"].Hidden = true;
                }
            }
        }
        void InitializeDropDowns()
        {
            UltraDropDown uddEstado = DropDownsProvider.GetEstadoLecturaDropwDown().DropDown;
            this.ultraGridData.DisplayLayout.Bands[0].Columns["Estado"].ValueList = uddEstado;             
            ControlsThemeBuilder.DropDownMakeUp(uddEstado,ThemeColor.Amarillo);

            UltraDropDown uddControlador = DropDownsProvider.GetControladorDropwDown().DropDown;
            this.ultraGridData.DisplayLayout.Bands[0].Columns["NumControler"].ValueList = uddControlador;
            ControlsThemeBuilder.DropDownMakeUp(uddControlador, ThemeColor.Amarillo);
        }
        protected override void LoadData()
        {
            if(!codigoControlador.HasValue)
                base.LoadData();
            else
            {
                ILecturaControlService srvLecturaControl = AfcaServiceProvider.GetLecturaControlService();
                LecturaControlData loadedData= srvLecturaControl.LoadLecturaControlDataByParent(itemContainer);
                lecturaControlData.Clear();
                lecturaControlData.Merge(loadedData);
                ultraGridData.DataSource = DataSource;
                ultraGridData.DataMember = DataMember;
            }
        }
        protected override void UpdateData()
        {
            base.UpdateData();
            if (itemContainer != null)
                itemContainer.UserInterface.RefreshUI();
        }

        private void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "tbbVerLectura":
                    ControlsProvider.ShowLecturaProperties(
                      (((DataRowView)  ultraGridData.Selected.Rows[0].ListObject).Row));
                    break;
            }
        }
    }
}
