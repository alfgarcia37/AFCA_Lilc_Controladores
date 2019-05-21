using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkForge.UI;
using Afca.Common;

namespace Afca.UI
{
    public static class DropDownsProvider
    {
        public static DropDownControl GetEstadoLecturaDropwDown()
        {
            return new DropDowns.EstadoLecturaDropDown();
        }

        public static DropDownControl GetControladorDropwDown()
        {
            return new DropDowns.ControladorDropDown();
        }
    }
}
