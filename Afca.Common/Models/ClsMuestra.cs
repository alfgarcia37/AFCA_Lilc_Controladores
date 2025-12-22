using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Models
{
  public class Muestra
  {
    public string CodigoBarras { get; set; }
    public string? Establo { get; set; }// "J"
    public string? NombreControlador { get; set; }

    public string? FechaRegistro { get; set; }

    public string?FechaControlador { get; set; }
    public string? Recombi { get; set; } // pasar como "3"
    public string? Ptecombi { get; set; } /// pasar como "1"  -> devuelve 0 si esta procesado

    public Muestra(string codbarras)
    {
      this.CodigoBarras = codbarras ?? throw new ArgumentNullException(nameof(codbarras));
    }
  }
}
