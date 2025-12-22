#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Afca.Common.Models
{
  public class MuestraIndustria
  {
    public string CodigoGanadero { get; set; }
    public string NombreGanadero { get; set; }
    public string Origen { get; set; }
    public string Naturaleza { get; set; }
    //public int Ano { get; set; }
    //public int Mes { get; set; }
    public string? Periodo { get; set; }
    public string? Etiqueta { get; set; }
    public string? Industria { get; set; }
    public string? CodEnIndustria { get; set; }
    
    public string? FechaRecogida { get; set; }
    public string? FechaRecepcion { get; set; }
    public string? FechaAnalisis { get; set; }
    public string CodigoMuestra { get; set; }
    public string CodigoBarras { get; set; }
    public double? Grasa { get; set; }
    public double? Proteina { get; set; }
    public double? Lactosa { get; set; }
    public double? Extracto { get; set; }
    public double? Bacterias { get; set; }
    public double? RCS { get; set; }
    public string? Inhibidor { get; set; } // "S" or "N"
    public double? PuntoCrioscopico { get; set; }
    public double? Urea { get; set; }
    public double? Caseina { get; set; }
    public string Observaciones { get; set; }

    public double? MediaGrasa { get; set; }
    public double? MediaProteina { get; set; }
    public double? MediaLactosa { get; set; }
    public double? MediaExtracto { get; set; }
    public double? MediaBacterias { get; set; }
    public double? MediaCS { get; set; }
    public string? MediaInhibidor { get; set; } // "S" or "N"
    public double? MediaPuntoCrioscopico { get; set; }
    public double? MediaUrea { get; set; }

    public string? Establo { get; set; }// "J"
    public string? CEA { get; set; }
    public MuestraIndustria(string codigoBarras)
    {
      this.CodigoBarras = codigoBarras ?? throw new ArgumentNullException(nameof(codigoBarras));
    }
  }
}
