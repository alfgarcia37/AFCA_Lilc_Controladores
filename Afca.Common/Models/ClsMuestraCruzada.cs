using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Models
{
  public class MuestraCruzada
  {
    public string CodigoBarras { get; set; }
    public double? Grasa { get; set; }
    public double? Proteina { get; set; }
    public int? Lactosa { get; set; }
    public int? Urea { get; set; }
    public int? RCS { get; set; }
    public double? Acetona { get; set; }
    public double? Bhb { get; set; }
    public double? AC26 { get; set; }
    public double? AC27 { get; set; }
    public double? AC28 { get; set; }
    public double? AC29 { get; set; }
    public double? Caseina { get; set; }
    public string? FechaAnalisis { get; set; }
    public string FechaEntrega { get; set; }
    public int? Controlador { get; set; }
  }
}
