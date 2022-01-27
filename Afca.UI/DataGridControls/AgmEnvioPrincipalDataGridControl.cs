using Afca.Common.ServicesInterfaces;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using FrameworkForge.UI;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections;
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
    public partial class AgmEnvioPrincipalDataGridControl : DataGridControl
    {
        ItemData itemContainer;
        public AgmEnvioPrincipalDataGridControl(ItemData argItemContainer)
        {
            InitializeComponent();
            itemContainer = argItemContainer;
            LoadData();
        }
        protected override void InitializeAppearance()
        {
            base.InitializeAppearance();
            ControlsThemeBuilder.GridMakeUp(this.ultraGridData, ThemeColor.Amarillo);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[0], ThemeColor.Amarillo);
            ControlsThemeBuilder.GridBandMakeUp(this.ultraGridData.DisplayLayout.Bands[1], ThemeColor.Gris);

            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbCheckSelected", "IconoSelectAll");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbUncheckSelected", "IconoUnselectAll");
            ControlsTasks.SetToolBarButtonImage(ultraToolbarsManager, "tbbSend", "IconoPlay");

            foreach(string tbbkey in new string[] {"tbbAddNew","tbbDelete","tbbSave" })
                    ultraToolbarsManager.Tools[tbbkey].SharedProps.Enabled = false;
            


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
        void CheckSelected()
        {
            if (ultraGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow grow in ultraGridData.Selected.Rows)
                    CheckRegistry(grow);                

                ultraGridData.UpdateData();
            }
        }
        void CheckRegistry(UltraGridRow grow)
        {
            if (grow.Cells["Status"].Value.ToString() == "PE" ||
                        grow.Cells["Status"].Value.ToString() == "ER")
            {
                grow.Cells["Selected"].Value = true;

                if (grow.Band.Index == 1
                    && grow.ParentRow.Cells["Status"].Value.ToString() == "OK")
                    grow.ParentRow.Cells["Selected"].Value = true;
            }
        }
        void UncheckSelected()
        {
            if (ultraGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow grow in ultraGridData.Selected.Rows)
                    UncheckRegistry(grow);

                ultraGridData.UpdateData();
            }
        }
        void UncheckRegistry(UltraGridRow grow)
        {
            if (grow.Cells["Status"].Value.ToString() == "PE")
                grow.Cells["Selected"].Value = false;
        }
        async Task SendData()
        {
            ultraGridData.UpdateData();

            IAgmService agmService = ((IAgmService)Service);

            var ganaderiasToSend = agmEnvioData.Ganaderia.Where(g => g.Selected);

            foreach(var ganaderia in ganaderiasToSend)
            {
                var entidades = agmEnvioData.Detail
                    .Where(d => d.GanaderiaID == ganaderia.GanaderiaID && d.Selected);

                if(entidades!=null && entidades.Count() > 0)
                {
                    ArrayList entidadesIncluded = new ArrayList();
                    entidadesIncluded.AddRange(agmEnvioData.Detail
                    .Where(d => d.GanaderiaID == ganaderia.GanaderiaID && d.Selected)
                    .Select(e => e.Entidad).ToArray());

                    string message = agmService
                       .GenerateJsonMessageEnvioPrincipal(ganaderia.GanaderiaID, entidadesIncluded);

                    if (!String.IsNullOrEmpty(message))
                    {
                        var result = await agmService.SendJsonMessageEnvioPrincipal(ganaderia.GanaderiaID, message).ConfigureAwait(false);

                        string status = result.success ? "OK" : "ER";

                        ganaderia.Status = status;
                        ganaderia.Selected = false;
                        ganaderia.Message = result.success ? "Envio aceptado" : result.value;

                        foreach (var entidad in entidades)
                        {
                            entidad.Status = status;
                            entidad.Selected = false;
                        }

                        if (result.success)
                        {
                            //call procedure to mark all ganaderia rows as sended                            
                        }
                        else
                        {
                            //mark as err??
                        }
                    }
                    else
                    {
                        ganaderia.Message = "Sin información asociada para enviar.";
                    }
                }

               
                // prepare json data ; parameters : ganaderiaId, entidades selected
                // send data 
                // if ( ok)  -> mark as updated in database; change status in dataset
                // if (!ok)  -> do nothing in database (log de operaciones?), change status in dataset (show message)
            }
        }
        private async void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "tbbCheckSelected":
                    CheckSelected();
                    break;
                case "tbbUncheckSelected":
                    UncheckSelected();
                    break;
                case "tbbSend":
                    await SendData();
                    break;
            }
        }
    }
}
