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
using Infragistics.Win.UltraWinToolbars;
using dO = FrameworkForge.Common.DataOperations;

namespace Afca.UI
{
    public partial class LecturaControlDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        int? codigoControlador;
        string nombreControlador = "";

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

            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbVerLectura", "IconoPropiedades");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbNextLote", "IconoNext");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbGenerar", "IconoExport");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbImportar", "IconoImport");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "popMenuEstados", "IconoChange"); 


            ControlsThemeBuilder.GridMakeUp(this.ultraGridData,ThemeColor.Verde);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0],ThemeColor.Verde);
            SetDataGridFilters();

            if (codigoControlador.HasValue)
            {
                if(Parent!=null && Parent is Form &&
                    itemContainer!=null && itemContainer.ItemColumns.ContainsKey("Nombre"))
                {
                    nombreControlador  = itemContainer.ItemColumns["Nombre"].Data.Value.ToString();
                    ((Form)Parent).Text = nombreControlador;
                    ultraGridData.DisplayLayout.Bands[0].Columns["NumControler"].Hidden = true;
                }
            }

            string[] menukeys = new string[] {"menuPlanificado","menuLeido","menuEnCurso","menuEnTerminal","menuEnBuzon" };
        
            foreach(string mkey in menukeys)
            {
                if (ultraToolbarsManager.Tools.IndexOf(mkey) != -1)
                {
                    if (ultraToolbarsManager.Tools[mkey].Tag != null)
                    {
                        ToolBase menu = ultraToolbarsManager.Tools[mkey];

                        int value = 0;
                        if (int.TryParse(menu.Tag.ToString(),out value)){
                            menu.SharedProps.AppearancesSmall.Appearance.Image=
                                FrameworkForge.Common.ResourceProvider.GetIconImage(IconMapper.GetAfcaIconName((EstadoLectura)value));
                        }
                    }
                }
            }

        }
        void InitializeDropDowns()
        {
            this.ultraGridData.DisplayLayout.Bands[0].Columns["Estado"].ValueList = DataMapper.GetAfcaEstadoIconValueList();

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

        private void ultraGridData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            OpenLecturaPropertiesControl(e.Row);
        }
        private void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            string[] menukeys = new string[] { "menuPlanificado", "menuLeido", "menuEnCurso", "menuEnTerminal", "menuEnBuzon" };

            switch (e.Tool.Key)
            {
                case "tbbVerLectura":
                    OpenLecturaPropertiesControl();
                    break;
                case "tbbNextLote":
                    EjecutarComando("SiguienteLote");
                    break;
                case "tbbGenerar":
                    EjecutarComando("Generar");
                    break;
                case "tbbImportar":
                    EjecutarComando("Importar");
                    break;
                default:
                    if(e.Tool.Key.StartsWith("menu") && ultraToolbarsManager.Tools[e.Tool.Key].Tag != null)
                        ChangeEstado(int.Parse(ultraToolbarsManager.Tools[e.Tool.Key].Tag.ToString()));
                    break;
            }
        }
        void OpenLecturaPropertiesControl(UltraGridRow row=null)
        {
            Cursor = Cursors.WaitCursor;
            ControlsProvider.ShowLecturaProperties(
                row==null?
              ((DataRowView)ultraGridData.Selected.Rows[0].ListObject).Row:
              ((DataRowView)row.ListObject).Row);
            Cursor = Cursors.Default;
        }
        void ChangeEstado(int newEstado)
        {
            if (ultraGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugrow in ultraGridData.Selected.Rows)
                {
                    ugrow.Cells["Estado"].Value = newEstado;
                    ugrow.Update();
                }
            }
        }
        void EjecutarComando(string comando)
        {
            int controladorID = -1;
            string controladorName = "";

            if (codigoControlador.HasValue)
            {
                controladorID = codigoControlador.Value;
                controladorName = dO.cls(nombreControlador, codigoControlador.Value.ToString()).ToString();
            }
            else
            {
                if (ultraGridData.Selected.Rows.Count == 1)
                {
                    controladorID = int.Parse(ultraGridData.Selected.Rows[0].Cells["NumControler"].Value.ToString());
                    controladorName = ultraGridData.Selected.Rows[0].Cells["NumControler"].Text;
                }
            }

            if (controladorID == -1) return;

            if (lecturaControlData.HasChanges())
            {
                DialogsManager.ShowInfo("Aviso", "Antes de continuar ha de guardar los cambios.");
                return;
            }

            string message = "";

            switch (comando)
            {
                case "SiguienteLote":
                    message = "Se va a ejecutar el SIGUIENTE LOTE";
                    break;
                case "Generar":
                    message = "Se van a GENERAR las lecturas";
                    break;
                case "Importar":
                    message = "Se van a IMPORTAR las lecturas";
                    break;
            }

            if (MessageBox.Show(string.Format("{0}{1}{2}¿ Desea continuar ?",
                   message,Environment.NewLine, Environment.NewLine), controladorName, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                switch (comando)
                {
                    case "SiguienteLote":
                        ControlsProvider.SetLecturasSiguienteLoteByID(controladorID);
                        break;
                    case "Generar":
                        ControlsProvider.GenerarLecturasControladorByID(controladorID);
                        break;
                    case "Importar":
                        ControlsProvider.ImportarLecturasControladorByID(controladorID);
                        break;
                }
                LoadData();
                Cursor = Cursors.Default;
            }
        }
    }
}
