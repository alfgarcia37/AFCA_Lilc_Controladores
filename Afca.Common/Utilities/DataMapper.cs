using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common
{
    public static class DataMapper
    {
        public  static string GetEstadoLecturaText(EstadoLectura estado)
        {
            string name = "";

            switch (estado)
            {
                case EstadoLectura.Leido:
                    name = "Leida";
                    break;
                case EstadoLectura.Planificar:
                    name = "Planificada";
                    break;
                case EstadoLectura.EnCurso:
                    name = "En Curso";
                    break;
                case EstadoLectura.Replanificado:
                    name = "Replanificada";
                    break;
                case EstadoLectura.NoLeido:
                    name = "No Leida";
                    break;
                case EstadoLectura.EnTerminal:
                    name = "En Terminal";
                    break;
                case EstadoLectura.EnBuzon:
                    name = "En Buzón";
                    break;
                default:
                    return "";
            }

            return name.ToUpper();
        }
        public static ValueList GetAfcaEstadoIconValueList()
        {
            ValueList vlist = new ValueList();

            foreach (var item in Enum.GetValues(typeof(EstadoLectura)))
            {
                EstadoLectura estado = (EstadoLectura)item;
                ValueListItem listItem = vlist.ValueListItems.Add((int)estado, GetEstadoLecturaText(estado));
                listItem.Appearance.Image = FrameworkForge.Common.ResourceProvider.GetIconImage(IconMapper.GetAfcaIconName(estado));            
            }
            return vlist;
        }
    }
}
