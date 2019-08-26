using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common
{
    public static class IconMapper
    {
        public static string GetAfcaIconName(EstadoLectura estado)
        {
            string iconName = "";

            switch (estado)
            {
                case EstadoLectura.Leido:
                    iconName = "IconoLecturaLeida";
                    break;
                case EstadoLectura.Planificar:
                    iconName = "IconoLecturaPlanificada";
                    break;
                case EstadoLectura.EnCurso:
                    iconName = "IconoLecturaEnCurso";
                    break;
                case EstadoLectura.Replanificado:
                    iconName = "IconoLecturaReplanificada";
                    break;
                case EstadoLectura.NoLeido:
                    iconName = "IconoLecturaNoLeida";
                    break;
                case EstadoLectura.EnTerminal:
                    iconName = "IconoLecturaEnTerminal";
                    break;
                default:
                    return "";
            }

            return iconName;
        }
    }
}
