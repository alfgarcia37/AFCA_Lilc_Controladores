using Afca.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

public static class LaboratorioTanqueRepository
{
  public static void InsertarMuestrasTanquePorSP( IEnumerable<MuestraIndustria> muestrasIndustria)
  {
    SqlConnection cn = null;

    try
    {
      var constring = ConfigurationManager.ConnectionStrings["controllechero"]?.ConnectionString;
      cn = new SqlConnection(constring);
      cn.Open();

      foreach (var muestra in muestrasIndustria.Take(5000))
      {
        // Variables tipadas (valores reales del modelo)
        short asociacionId = 1;
        string laboratorioId = "1";
        string ruta = "Ruta";
        string fichero = "mediante API";
        DateTime fechaRegistro = DateTime.Now;

        string id = (muestra != null) ? muestra.CodigoBarras : null;
        string establo = (muestra != null) ? muestra.Establo : null;
        string cea = (muestra != null) ? muestra.CEA : null;

        string etiqueta = (muestra != null) ? muestra.Etiqueta : null;
        string industria = (muestra != null) ? muestra.Industria : null;
        string codEnIndustria = (muestra != null) ? muestra.CodEnIndustria : null;

        string naturaleza = (muestra != null) ? muestra.Naturaleza : null;
        string origen = (muestra != null) ? muestra.Origen : null;

        DateTime? fechaRecogida = ParseFecha((muestra != null) ? muestra.FechaRecogida : null);

        double? grasa = (muestra != null) ? muestra.Grasa : null;
        double? proteina = (muestra != null) ? muestra.Proteina : null;
        double? lactosa = (muestra != null) ? muestra.Lactosa : null;
        double? extractoSeco = (muestra != null) ? muestra.Extracto : null;

        double? rcs = (muestra != null) ? muestra.RCS : null;
        double? bacterias = (muestra != null) ? muestra.Bacterias : null;
        double? urea = (muestra != null) ? muestra.Urea : null;

        double? pc = (muestra != null) ? muestra.PuntoCrioscopico : null;
        double varPc = 0;
        string igg = "0";

        bool inhibidores = (muestra != null && muestra.Inhibidor == "S");
        bool cierre = false;

        int ano = 0;
        int mes = 0;

        string nc = (muestra != null) ? muestra.Observaciones : null;

        SqlCommand cmd = null;

        try
        {
          cmd = new SqlCommand("dbo.sp_BuzonLaboratorioTanque_insert", cn);
          cmd.CommandType = CommandType.StoredProcedure;

          // --- Parámetros ---
          cmd.Parameters.Add("@AsociacionID", SqlDbType.SmallInt).Value = asociacionId;
          cmd.Parameters.Add("@LaboratorioID", SqlDbType.Char, 1).Value = laboratorioId;

          cmd.Parameters.Add("@Id", SqlDbType.Char, 14).Value = DbStr(id);
          cmd.Parameters.Add("@Establo", SqlDbType.Char, 7).Value = DbStr(establo);
          cmd.Parameters.Add("@CEA", SqlDbType.Char, 14).Value = DbStr(cea);
          cmd.Parameters.Add("@FechaRecogida", SqlDbType.SmallDateTime).Value = DbDate(fechaRecogida);

          cmd.Parameters.Add("@Etiqueta", SqlDbType.VarChar, 14).Value = DbStr(etiqueta);
          cmd.Parameters.Add("@Industria", SqlDbType.VarChar, 6).Value = DbStr(industria);
          cmd.Parameters.Add("@CodEnIndustria", SqlDbType.VarChar, 14).Value = DbStr(codEnIndustria);

          cmd.Parameters.Add("@Naturaleza", SqlDbType.Char, 1).Value = DbStr(naturaleza);
          cmd.Parameters.Add("@Origen", SqlDbType.Char, 1).Value = DbStr(origen);
          cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, 50).Value = ruta;

          cmd.Parameters.Add("@Grasa", SqlDbType.Real).Value = DbReal(grasa);
          cmd.Parameters.Add("@Proteina", SqlDbType.Real).Value = DbReal(proteina);
          cmd.Parameters.Add("@Lactosa", SqlDbType.Real).Value = DbReal(lactosa);

          cmd.Parameters.Add("@Rcs", SqlDbType.Int).Value = DbInt(rcs);
          cmd.Parameters.Add("@Bacterias", SqlDbType.Int).Value = DbInt(bacterias);

          cmd.Parameters.Add("@Urea", SqlDbType.Int).Value = DbInt(urea);
          cmd.Parameters.Add("@ExtractoSeco", SqlDbType.Real).Value = DbReal(extractoSeco);

          cmd.Parameters.Add("@MediaGrasa", SqlDbType.Real).Value = (float)0;
          cmd.Parameters.Add("@MediaProteina", SqlDbType.Real).Value = (float)0;
          cmd.Parameters.Add("@MediaLactosa", SqlDbType.Real).Value = (float)0;

          cmd.Parameters.Add("@MediaRcs", SqlDbType.Int).Value = 0;
          cmd.Parameters.Add("@MediaBacterias", SqlDbType.Int).Value = 0;
          cmd.Parameters.Add("@MediaUrea", SqlDbType.Int).Value = 0;

          cmd.Parameters.Add("@MediaExtractoSeco", SqlDbType.Real).Value = (float)0;
          cmd.Parameters.Add("@MediaPC", SqlDbType.Int).Value = 0;

          cmd.Parameters.Add("@IgG", SqlDbType.VarChar, 6).Value = igg;
          cmd.Parameters.Add("@PC", SqlDbType.Int).Value = DbInt(pc);
          cmd.Parameters.Add("@VarPC", SqlDbType.Real).Value = (float)varPc;
          
          cmd.Parameters.Add("@Inhibidores", SqlDbType.Bit).Value = inhibidores;
          cmd.Parameters.Add("@Cierre", SqlDbType.Bit).Value = cierre;

          cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = ano;
          cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = mes;

          cmd.Parameters.Add("@NC", SqlDbType.VarChar, 500).Value = DbStr(nc);
          cmd.Parameters.Add("@Fichero", SqlDbType.VarChar, 50).Value = fichero;
          cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = fechaRegistro;

          cmd.ExecuteNonQuery();
        }
        finally
        {
          if (cmd != null)
          {
            cmd.Dispose();
          }
        }
      }
      //fin for -> emparejo laboratorio tanque
      SqlCommand cmd1 = new SqlCommand("SP_BuzonLaboratorioTanqueEmparejar", cn);
      cmd1.CommandType = CommandType.StoredProcedure;
      cmd1 .ExecuteNonQuery();
      if (cmd1 != null)
      {
        cmd1.Dispose();
      }
    }
    finally
    {
      if (cn != null)
      {
        cn.Dispose();
      }
    }
  }

  public static void InsertarMediasTanquePorSP(IEnumerable<MuestraIndustria> muestrasIndustria)
  {
    SqlConnection cn = null;

    try
    {
      var constring = ConfigurationManager.ConnectionStrings["controllechero"]?.ConnectionString;
      cn = new SqlConnection(constring);
      cn.Open();

      foreach (var muestra in muestrasIndustria.Take(2000))
      {
        // Variables tipadas (valores reales del modelo)
        short asociacionId = 1;
        string laboratorioId = "1";
        string ruta = "Ruta";
        string fichero = "mediante API";
        DateTime fechaRegistro = DateTime.Now;

        string id = (muestra.CodigoBarras != null) ? muestra.CodigoBarras : null;
        string establo = (muestra.Establo != null) ? muestra.Establo : null;
        string cea = (muestra.CEA != null) ? muestra.CEA : null;

        string etiqueta = (muestra.Etiqueta != null) ? muestra.Etiqueta : null;
        string industria = (muestra.Industria != null) ? muestra.Industria : null;
        string codEnIndustria = (muestra.CodEnIndustria != null) ? muestra.CodEnIndustria : null;
        string naturaleza = (muestra.Naturaleza != null) ? muestra.Naturaleza : null;
        string origen = (muestra.Origen != null) ? muestra.Origen : null;

        DateTime? fechaRecogida = ParseFecha((muestra != null) ? muestra.FechaRecogida : null);

        double? grasa = (muestra.Grasa != null) ? muestra.Grasa : null;
        double? proteina = (muestra.Proteina != null) ? muestra.Proteina : null;
        double? lactosa = (muestra.Lactosa != null) ? muestra.Lactosa : null;
        double? extractoSeco = (muestra.Extracto != null) ? muestra.Extracto : 0;

        double? mediagrasa= (muestra.MediaGrasa != null) ? muestra.MediaGrasa : 0;
        double? mediaproteina = (muestra.MediaProteina != null) ? muestra.MediaProteina : 0;
        double? medialactosa = (muestra.MediaLactosa != null) ? muestra.MediaLactosa : 0;
        
        double? mediarcs = (muestra.MediaCS != null) ? muestra.MediaCS : 0;
        double? mediabacterias = (muestra.MediaBacterias != null) ? muestra.MediaBacterias : 0;
        double? mediaurea = (muestra.MediaUrea != null) ? muestra.MediaUrea : 0;
        double? mediaextracto = (muestra.MediaExtracto != null) ? muestra.MediaExtracto : 0;
        double? mediapc = (muestra.MediaPuntoCrioscopico != null) ? muestra.MediaPuntoCrioscopico : 0;

        double? rcs = (muestra.RCS != null) ? muestra.RCS : null;
        double? bacterias = (muestra.Bacterias != null) ? muestra.Bacterias : null;
        double? urea = (muestra.Urea != null) ? muestra.Urea : null;

        double? pc = (muestra.PuntoCrioscopico != null) ? muestra.PuntoCrioscopico : null;
        double varPc = 0;
        string igg = "0";

        bool inhibidores = (muestra.Inhibidor != null && muestra.Inhibidor == "S");
        bool cierre = true;
     
        int ano = int.Parse(muestra.Periodo.Substring(3, 4));
        int mes = int.Parse(muestra.Periodo.Substring(0, 2)); ;

        string nc = "0";

        SqlCommand cmd = null;

        try
        {
          cmd = new SqlCommand("dbo.sp_BuzonLaboratorioTanque_insert", cn);
          cmd.CommandType = CommandType.StoredProcedure;

          // --- Parámetros ---
          cmd.Parameters.Add("@AsociacionID", SqlDbType.SmallInt).Value = asociacionId;
          cmd.Parameters.Add("@LaboratorioID", SqlDbType.Char, 1).Value = laboratorioId;

          cmd.Parameters.Add("@Id", SqlDbType.Char, 14).Value = DbStr(id);
          cmd.Parameters.Add("@Establo", SqlDbType.Char, 7).Value = DbStr(establo);
          cmd.Parameters.Add("@CEA", SqlDbType.Char, 14).Value = DbStr(cea);
          cmd.Parameters.Add("@FechaRecogida", SqlDbType.SmallDateTime).Value = DbDate(fechaRecogida);

          cmd.Parameters.Add("@Etiqueta", SqlDbType.VarChar, 14).Value = DbStr(etiqueta);
          cmd.Parameters.Add("@Industria", SqlDbType.VarChar, 6).Value = DbStr(industria);
          cmd.Parameters.Add("@CodEnIndustria", SqlDbType.VarChar, 14).Value = DbStr(codEnIndustria);

          cmd.Parameters.Add("@Naturaleza", SqlDbType.Char, 1).Value = DbStr(naturaleza);
          cmd.Parameters.Add("@Origen", SqlDbType.Char, 1).Value = DbStr(origen);
          cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, 50).Value = ruta;

          cmd.Parameters.Add("@Grasa", SqlDbType.Real).Value = DbReal(grasa);
          cmd.Parameters.Add("@Proteina", SqlDbType.Real).Value = DbReal(proteina);
          cmd.Parameters.Add("@Lactosa", SqlDbType.Real).Value = DbReal(lactosa);

          cmd.Parameters.Add("@Rcs", SqlDbType.Int).Value = DbInt(rcs);
          cmd.Parameters.Add("@Bacterias", SqlDbType.Int).Value = DbInt(bacterias);

          cmd.Parameters.Add("@Urea", SqlDbType.Int).Value = DbInt(urea);
          cmd.Parameters.Add("@ExtractoSeco", SqlDbType.Real).Value = DbReal(extractoSeco);

          cmd.Parameters.Add("@MediaGrasa", SqlDbType.Real).Value = DbReal(mediagrasa);
          cmd.Parameters.Add("@MediaProteina", SqlDbType.Real).Value = DbReal(mediaproteina);
          cmd.Parameters.Add("@MediaLactosa", SqlDbType.Real).Value = DbReal(medialactosa);

          cmd.Parameters.Add("@MediaRcs", SqlDbType.Int).Value = DbInt(mediarcs);
          cmd.Parameters.Add("@MediaBacterias", SqlDbType.Int).Value = DbInt(mediabacterias);
          cmd.Parameters.Add("@MediaUrea", SqlDbType.Int).Value = DbInt(mediaurea);

          cmd.Parameters.Add("@MediaExtractoSeco", SqlDbType.Real).Value = DbReal(mediaextracto);
          cmd.Parameters.Add("@MediaPC", SqlDbType.Int).Value = DbInt(mediapc);

          cmd.Parameters.Add("@IgG", SqlDbType.VarChar, 6).Value = igg;
          cmd.Parameters.Add("@PC", SqlDbType.Int).Value = DbInt(pc);
          cmd.Parameters.Add("@VarPC", SqlDbType.Real).Value = (float)varPc;

          cmd.Parameters.Add("@Inhibidores", SqlDbType.Bit).Value = inhibidores;
          cmd.Parameters.Add("@Cierre", SqlDbType.Bit).Value = cierre;

          cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = ano;
          cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = mes;

          cmd.Parameters.Add("@NC", SqlDbType.VarChar, 500).Value = DbStr(nc);
          cmd.Parameters.Add("@Fichero", SqlDbType.VarChar, 50).Value = fichero;
          cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = fechaRegistro;

          cmd.ExecuteNonQuery();
        }
        finally
        {
          if (cmd != null)
          {
            cmd.Dispose();
          }
        }
      }
      //fin for -> emparejo laboratorio tanque
      SqlCommand cmd1 = new SqlCommand("SP_BuzonLaboratorioTanqueEmparejar", cn);
      cmd1.CommandType = CommandType.StoredProcedure;
      cmd1.ExecuteNonQuery();
      if (cmd1 != null)
      {
        cmd1.Dispose();
      }
    }
    finally
    {
      if (cn != null)
      {
        cn.Dispose();
      }
    }
  }

  // ---------------- Helpers ----------------

  private static object DbStr(string s)
  {
    return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;
  }

  private static object DbDate(DateTime? dt)
  {
    return dt.HasValue ? (object)dt.Value : DBNull.Value;
  }

  // SQL REAL => Single
  private static object DbReal(double? v)
  {
    return v.HasValue ? (object)(float)v.Value : DBNull.Value;
  }

  // SQL INT (ojo: Convert.ToInt32 redondea; si quieres truncado, usa (int)v.Value)
  private static object DbInt(double? v)
  {
    return v.HasValue ? (object)Convert.ToInt32(v.Value) : DBNull.Value;
  }

  private static DateTime? ParseFecha(string s)
  {
    if (string.IsNullOrWhiteSpace(s)) return null;

    string[] formats =
    {
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "dd/MM/yyyy",
            "dd/MM/yyyy HH:mm:ss",
            "MM-dd-yyyy",
            "MM-dd-yyyy HH:mm:ss",
            "dd-MM-yyyy",
            "dd-MM-yyyy HH:mm:ss",
            "yyyy/MM/dd",
            "yyyy/MM/dd HH:mm:ss"
        };

    DateTime dt;
    if (DateTime.TryParseExact(
            s.Trim(),
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal,
            out dt))
      return dt;

    if (DateTime.TryParse(s, CultureInfo.GetCultureInfo("es-ES"), DateTimeStyles.AssumeLocal, out dt))
      return dt;

    return null;
  }
}

