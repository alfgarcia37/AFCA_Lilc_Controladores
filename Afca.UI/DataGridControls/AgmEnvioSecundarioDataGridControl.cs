using Afca.Common;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using FrameworkForge.UI;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afca.UI
{
    public partial class AgmEnvioSecundarioDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        Task uiTask;
        UltraGridRow currentSendingRow;

        public AgmEnvioSecundarioDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            LoadData();
        }
        protected override void InitializeAppearance()
        {
            base.InitializeAppearance();
            ControlsThemeBuilder.GridMakeUp(this.ultraGridData, ThemeColor.Naranja);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0], ThemeColor.Naranja);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[1], ThemeColor.Gris);

            //ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbCheckSelected", "IconoSelectAll");
            //ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbUncheckSelected", "IconoUnselectAll");
            //ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbSend", "IconoPlay");

            //foreach (string tbbkey in new string[] { "tbbAddNew", "tbbDelete", "tbbSave" })
            //    ultraToolbarsManager.Tools[tbbkey].SharedProps.Enabled = false;



            foreach (UltraGridBand bi in ultraGridData.DisplayLayout.Bands)
            {
                if (!bi.CardView)
                {
                    foreach (UltraGridColumn col in bi.Columns)
                        col.PerformAutoResize();
                }
                else
                    bi.PerformAutoResizeCards(false);
            }
        }
        protected override void LoadData()
        {
            if (agmEnvioData != null)
                agmEnvioData.Clear();

            IAgmService agmService = AfcaServiceProvider.GetAgmService(); ;

            AgmEnvioData loadedData=null;

            switch (itemContainer.Name)
            {
                case "AgmEnviosRecibos":
                    loadedData  = agmService.GetAgmEnviosSecundariosToSend("Recibos");
                    break;
            }

            if (loadedData != null)
            {
                agmEnvioData.Merge(loadedData);
                this.ultraGridData.DataSource = agmEnvioData;
                this.ultraGridData.DataMember = agmEnvioData.Ganaderia.TableName;
                this.ultraGridData.DataBind();
            }

        }
    }
}
