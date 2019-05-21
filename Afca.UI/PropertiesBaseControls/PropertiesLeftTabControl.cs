using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrameworkForge.UI;
using FrameworkForge.Common;


namespace Afca.UI.PropertiesBaseControls
{
    public partial class PropertiesLeftTabControl : PropertiesControl
    {
        public PropertiesLeftTabControl()
        {
            InitializeComponent();
        }
        protected void SetPropertiesControlImage(string iconName)
        {
            if (Parent is Form)
            {
                ((Form)Parent).Icon = ResourceProvider.GetIcon(iconName);
                ((Form)Parent).Focus();
                ((Form)Parent).Refresh();
            }
        }
    }
}
