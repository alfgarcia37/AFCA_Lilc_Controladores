using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Models
{
    public partial class Rcs
    {

        public short AsociacionId { get; set; }
        public string GanaderiaId { get; set; }
        public string CodigoBarras { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaCreada { get; set; }
        public DateTime? FechaDescarga { get; set; }
        public DateTime? FechaEntregaGanadero { get; set; }
        public DateTime? FechaEntregaControlador { get; set; }
        public DateTime? FechaAnalisis { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int Celulas { get; set; }
        public int Controlador { get; set; }
       
    }
}
