using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Afca.UI.PropertiesBaseControls;
using Afca.Common.ServicesInterfaces;
using Afca.Common;
using Afca.Common.DataSets;
using FrameworkForge.Common;
using dO = FrameworkForge.Common.DataOperations;
using FrameworkForge.Common.DataStructures;

namespace Afca.UI
{
    public partial class LecturaPropertiesControl : PropertiesLeftTabControl
    {
        #region variables
        ItemData itemLectura;
        string establoId;
        string estadoId = "";
        CurrencyManager cmLectura;
        IEstadoLecturaService srvEstado;
        ILecturaControlService srvLectura;

        #endregion

        #region propiedades
        #endregion

        #region constructor
        public LecturaPropertiesControl(string argEstabloId,string argEstadoId)
        {
            InitializeComponent();
            establoId = argEstabloId;
            estadoId = argEstadoId;
            IsNew = false;
            InitializeControlsAppearance();
            InitializeCurrencyManagers();
            InitializeDataBindigs();
            InitializeServices();
            InitializeDropDowns();
            LoadData();
        }
        public LecturaPropertiesControl(ItemData argItemLectura)
        {
            InitializeComponent();
            itemLectura = argItemLectura;
            establoId = argItemLectura.ItemColumns["Establo"].Data.Value.ToString();
            estadoId = argItemLectura.ItemColumns["Estado"].Data.Value.ToString();
            IsNew = false;
            InitializeControlsAppearance();
            InitializeCurrencyManagers();
            InitializeDataBindigs();
            InitializeServices();
            InitializeDropDowns();
            LoadData();
        }
        #endregion

        #region métodos de inicio
        public override void InitializeControlsAppearance()
        {
            if (!this.DesignMode)
            {
                SetIconEstadoAppearance();
                ControlsThemeBuilder.GroupBoxMakeUp(ugroupEstablo, ThemeColor.Azul);
                ControlsThemeBuilder.GroupBoxMakeUp(ugroupSituacion, ThemeColor.Azul);
                ControlsThemeBuilder.GroupBoxMakeUp(ugroupFechas, ThemeColor.Azul);
                ControlsTasks.SetGroupBoxAppearanceImage(ugroupFechas, "IconoCalendario");
                ControlsThemeBuilder.ComboMakeUp(ucEstado, ThemeColor.Amarillo);
            }
        }
        void SetIconEstadoAppearance()
        {
            if (!String.IsNullOrEmpty(estadoId))
            {
                int estadoValue = int.Parse(estadoId);
                string iconName = "";

                iconName = IconMapper.GetAfcaIconName((EstadoLectura)estadoValue);

                if (!String.IsNullOrEmpty(iconName))
                {
                    SetPropertiesControlImage(iconName);
                    ControlsTasks.SetTabAppearanceImage(this.ultraTabControl, "tabGeneral", iconName);
                }
            }
        }
        public override void InitializeCurrencyManagers()
        {
            cmLectura = (CurrencyManager)this.BindingContext[this.lecturaControlData, "LecturaControl"];
        }
        public override void InitializeDataBindigs()
        {
            uteNombre.DataBindings.Add("Text", this.lecturaControlData, "LecturaControl.EstabloNombre");
            uteEstablo.DataBindings.Add("Text", this.lecturaControlData, "LecturaControl.Establo");
            uneNumLote.DataBindings.Add("Value", this.lecturaControlData, "LecturaControl.NumLote");
            uneVacasControl.DataBindings.Add("Value", this.lecturaControlData, "LecturaControl.VacasControl");
            ucEstado.DataBindings.Add("Value", lecturaControlData, "LecturaControl.Estado");
            uceTipoControl.DataBindings.Add("Value", lecturaControlData, "LecturaControl.TipoControl");
            udteFechaPrevista.DataBindings.Add("Value", lecturaControlData, "LecturaControl.FechaPrevista");
            udteFechaAlta.DataBindings.Add("Value", lecturaControlData, "LecturaControl.FechaAlta");
            udteFechaBaja.DataBindings.Add("Value", lecturaControlData, "LecturaControl.FechaBaja");
        }
        public override void InitializeServices()
        {
            srvEstado = AfcaServiceProvider.GetEstadoLecturaService();
            srvLectura = AfcaServiceProvider.GetLecturaControlService();
        }
        public override void InitializeDropDowns()
        {
            EstadoLecturaData tempData = srvEstado.LoadEstadoLecturaData();
            estadoLecturaData.Merge(tempData);
            ucEstado.DataSource = estadoLecturaData;
            ucEstado.DataMember = estadoLecturaData.EstadoLectura.TableName;
            ucEstado.DisplayMember = "Nombre";
            ucEstado.ValueMember = "EstadoID";
            ucEstado.DataBind();            
        }
        public override void LoadData()
        {
            lecturaControlData.Merge(srvLectura.LoadLecturaControlDataByID(establoId));
        }
        #endregion

        #region métodos finales
        void EndCurrentEdit()
        {
            cmLectura.EndCurrentEdit();
        }
        void UpdateUIs()
        {
            DataRow lecturaRow = lecturaControlData.LecturaControl.Rows[0];

            if (itemLectura != null)
            {
                string[] colsToUpdate = new string[] { "Estado", "FechaPrevista", "FechaAlta", "FechaBaja" };

                foreach (string col in colsToUpdate)
                {
                    var search = itemLectura.ItemColumns.Where(ic => ic.Key == col).FirstOrDefault().Value;

                    if (search != null)
                        search.Data.Value = lecturaRow[col];
                }
                itemLectura.UserInterface.RefreshUI();
            }

            estadoId = lecturaRow["Estado"].ToString();
            SetIconEstadoAppearance();
        }
        public override void Aceptar()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.EndCurrentEdit();

                if (lecturaControlData.HasChanges())
                {
                    srvLectura.UpdateLecturaControlData(lecturaControlData);
                    UpdateUIs();
                }

                base.Aceptar();
                this.Cursor = Cursors.Default;
            }
            catch(System.Exception e)
            {
                this.Cursor = Cursors.Default;
                DialogsManager.ShowInfo("Error", string.Format("Se ha producido un error al guardar:\n{0}", e.Message));

            }
        }
        #endregion
    }
}
