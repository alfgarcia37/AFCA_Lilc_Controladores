using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Afca.Common.Models
{
  public class RegistroConafe
  {
    [JsonProperty("numero", Required = Required.Always)]
    public string Numero { get; set; }

    [JsonProperty("aut", Required = Required.Always)]
    public string Aut { get; set; }

    [JsonProperty("nombre", Required = Required.Always)]
    public string Nombre { get; set; }

    [JsonProperty("fnac", Required = Required.Always)]
    public string Fnac { get; set; }

    [JsonProperty("ganacr", Required = Required.Always)]
    public string Ganacr { get; set; }

    [JsonProperty("ganapr", Required = Required.Always)]
    public string Ganapr { get; set; }

    [JsonProperty("padre", Required = Required.Always)]
    public string Padre { get; set; }

    [JsonProperty("madre", Required = Required.Always)]
    public string Madre { get; set; }

    [JsonProperty("tipomov", Required = Required.Always)]
    public string Tipomov { get; set; }

    [JsonProperty("fechaMov", Required = Required.Always)]
    public string FechaMov { get; set; }

    [JsonProperty("numtran")]
    public string Numtran { get; set; }

    [JsonProperty("pureza")]
    public string Pureza { get; set; }

    [JsonProperty("et")]
    public string Et { get; set; }

    [JsonProperty("sexog")]
    public string Sexog { get; set; }

    [JsonProperty("capa")]
    public string Capa { get; set; }

    [JsonProperty("facpar")]
    public string Facpar { get; set; }

    [JsonProperty("tamcr")]
    public string Tamcr { get; set; }

    [JsonProperty("numero_Origen")]
    public string NumeroOrigen { get; set; }

    [JsonProperty("cib", Required = Required.Always)]
    public string Cib { get; set; }

    [JsonProperty("collar")]
    public string Collar { get; set; }

    [JsonProperty("fechaDoc")]
    public string FechaDoc { get; set; }

    [JsonProperty("esModif", Required = Required.Always)]
    public string EsModif { get; set; }


  }

  public class LactacionesConafe
  {
    [JsonProperty("aut", Required = Required.Always)]
    public string Aut { get; set; }

    [JsonProperty("numero", Required = Required.Always)]
    public string Numero { get; set; }

    [JsonProperty("numpar", Required = Required.Always)]
    public byte Numpar { get; set; }

    [JsonProperty("ganlact", Required = Required.Always)]
    public string Ganlact { get; set; }

    [JsonProperty("dias")]
    public short? Dias { get; set; }

    [JsonProperty("klnat")]
    public int? Klnat { get; set; }

    [JsonProperty("kgnat")]
    public short? Kgnat { get; set; }

    [JsonProperty("kpnat")]
    public short? Kpnat { get; set; }

    [JsonProperty("kl305")]
    public int? Kl305 { get; set; }

    [JsonProperty("kg305")]
    public short? Kg305 { get; set; }

    [JsonProperty("kp305")]
    public short? Kp305 { get; set; }

    [JsonProperty("fbaj")]
    public string Fbaj { get; set; }

    [JsonProperty("tipobaja")]
    public byte? Tipobaja { get; set; }

    [JsonProperty("fincurso", Required = Required.Always)]
    public string Fincurso { get; set; }
  }

  public class ControlesConafe
  {
    [JsonProperty("aut", Required = Required.Always)]
    public string Aut { get; set; }

    [JsonProperty("numero", Required = Required.Always)]
    public string Numero { get; set; }

    [JsonProperty("numpar", Required = Required.Always)]
    public byte Numpar { get; set; }

    [JsonProperty("fcontrol", Required = Required.Always)]
    public string Fcontrol { get; set; }

    [JsonProperty("metodo", Required = Required.Always)]
    public string Metodo { get; set; }

    [JsonProperty("leche")]
    public decimal? Leche { get; set; }

    [JsonProperty("grasa")]
    public decimal? Grasa { get; set; }

    [JsonProperty("proteina")]
    public decimal? Proteina { get; set; }

    [JsonProperty("rcs")]
    public int? Rcs { get; set; }

    [JsonProperty("lactosa")]
    public decimal? Lactosa { get; set; }

    [JsonProperty("urea")]
    public decimal? Urea { get; set; }

    [JsonProperty("bhb")]
    public decimal? Bhb { get; set; }

    [JsonProperty("acetona")]
    public decimal? Acetona { get; set; }
     

    [JsonProperty("fana")]
    public string Fana { get; set; }

    [JsonProperty("incidencia")]
    public byte? Incidencia { get; set; }

    [JsonProperty("controlador")]
    public long? Controlador { get; set; }
     

    [JsonProperty("ganlact", Required = Required.Always)]
    public string Ganlact { get; set; }
  }

  public class CubricionesConafe
  {
    [JsonProperty("aut", Required = Required.Always)]
    public string Aut { get; set; }

    [JsonProperty("hembra", Required = Required.Always)]
    public string Hembra { get; set; }

    [JsonProperty("fecha", Required = Required.Always)]
    public string Fecha { get; set; }

    [JsonProperty("tipo", Required = Required.Always)]
    public byte Tipo { get; set; }

    [JsonProperty("diagnostico")]
    public byte? Diagnostico { get; set; }

    [JsonProperty("fdiag")]
    public string Fdiag { get; set; }

    [JsonProperty("fbaj")]
    public string Fbaj { get; set; }

    [JsonProperty("ganaderia", Required = Required.Always)]
    public string Ganaderia { get; set; }

    [JsonProperty("inseminador")]
    public string Inseminador { get; set; }

    [JsonProperty("macho", Required = Required.Always)]
    public string Macho { get; set; }

    [JsonProperty("metodoDiagnostico")]
    public byte? MetodoDiagnostico { get; set; }

    [JsonProperty("marca")]
    public byte? Marca { get; set; }
  }

  public class PartosConafe
  {
    [JsonProperty("aut", Required = Required.Always)]
    public string Aut { get; set; }

    [JsonProperty("numero", Required = Required.Always)]
    public string Numero { get; set; }

    [JsonProperty("numpar", Required = Required.Always)]
    public byte Numpar { get; set; }

    [JsonProperty("fpar", Required = Required.Always)]
    public string Fpar { get; set; }

    [JsonProperty("tipo")]
    public byte? Tipo { get; set; }

    [JsonProperty("facpar")]
    public byte? Facpar { get; set; }

    [JsonProperty("tamcria")]
    public byte? Tamcria { get; set; }

    [JsonProperty("retplacenta")]
    public string Retplacenta { get; set; }

    [JsonProperty("ganpar")]
    public string Ganpar { get; set; }
  }

  // Clases para el contrato de respuesta de la API de CONAFE
  public class ErrorConafe
  {
    [JsonProperty("indice")]
    public int Indice { get; set; }

    [JsonProperty("campo")]
    public string Campo { get; set; }

    [JsonProperty("codigo")]
    public string Codigo { get; set; }

    [JsonProperty("mensaje")]
    public string Mensaje { get; set; }
  }

  public class RespuestaConafe
  {
    [JsonProperty("totalRecibidos")]
    public int TotalRecibidos { get; set; }

    [JsonProperty("insertados")]
    public int Insertados { get; set; }

    [JsonProperty("rechazados")]
    public int Rechazados { get; set; }

    [JsonProperty("errores")]
    public List<ErrorConafe> Errores { get; set; }

    public RespuestaConafe()
    {
      Errores = new List<ErrorConafe>();
    }
  }
}
