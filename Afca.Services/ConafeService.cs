using Afca.Common;
using Afca.Common.Models;
using Afca.Common.ServicesInterfaces;
using Afca.Common.Utilities;
using FrameworkForge.Common;
using Newtonsoft.Json; // Asegúrate de tener este using
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Afca.Services
{
  public class ConafeService : IConafeService
  {
    public HttpClient client= new HttpClient();
    public List<Muestra> ListaMuestras = new List<Muestra>();
    public List<RegistroConafe> ListaRegistrosConafe = new List<RegistroConafe>();
    public List<LactacionesConafe> ListaLactacionesConafe = new List<LactacionesConafe>();
    public List<PartosConafe> ListaPartosConafe = new List<PartosConafe>();
    public List<ControlesConafe> ListaControlesConafe = new List<ControlesConafe>();
    public List<CubricionesConafe> ListaCubricionesConafe = new List<CubricionesConafe>();
    public List<MuestraIndustria> Listaindustria = new List<MuestraIndustria>();
    public string FechaDesde { get; set; }
    public string FechaHasta { get; set; }
    string connectionString = ConfigurationManager.ConnectionStrings["controllechero"].ConnectionString;
    // connectionString ="https://localhost:44347/api/"
    public string directorioPayload { get; set; }
    string usuario = Constants.user;
    string pass = Constants.pass;

    public ConafeService()
    {

      #if DEBUG
            //ServicePointManager.ServerCertificateValidationCallback =
            //    new RemoteCertificateValidationCallback((sender, cert, chain, errors) => true); // SOLO DEV
      #endif
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiConfigPathConafe"]); //"https://sinbad.conafe.com/GESLIBEntradaWS" 

            client.DefaultRequestHeaders.Accept.Clear();
            FechaDesde = ConfigurationManager.AppSettings["FechaDesde"];
            FechaHasta = ConfigurationManager.AppSettings["FechaHasta"];
            ConafeConfig.SetAuthorizationHeaderConafe(client, Constants.MiTokenLelyGlobal);
            directorioPayload = ConfigurationManager.AppSettings["directorioPayload"];
    }

    //Registros añadidos o modificados de RegGen
    public async Task<RespuestaConafe> GetRegistrosConafe( )
    {
      string baseUrl = client.BaseAddress.ToString(); //https://sinbad.conafe.com/GESLIBEntradaAPI
      baseUrl = baseUrl + $"/animales/lote";
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand("Reggen..SP_ConafeAPI_Animales", connection))
          {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 0;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var ganapr = reader["ganapr"] == DBNull.Value ? "" : reader["ganapr"].ToString().Trim();
                var tipomov = reader["tipomov"] == DBNull.Value ? "" : reader["tipomov"].ToString().Trim();

                if (!string.IsNullOrEmpty(ganapr) && (ganapr.StartsWith("V") || ganapr.StartsWith("M")))
                {
                  tipomov = "03";
                }

                var registro = new RegistroConafe()
                {
                  Aut = reader["aut"] == DBNull.Value ? "" : reader["aut"].ToString().Trim(),
                  Numero = reader["numero"] == DBNull.Value ? "" : reader["numero"].ToString().Trim(),
                  Nombre = reader["nombre"] == DBNull.Value ? "" : reader["nombre"].ToString().Trim(),
                  Fnac = reader["fnac"] == DBNull.Value ? "" : reader["fnac"].ToString().Trim(),
                  Ganacr = reader["ganacr"] == DBNull.Value ? "" : reader["ganacr"].ToString().Trim(),
                  Ganapr = ganapr,
                  Padre = reader["padre"] == DBNull.Value ? "" : reader["padre"].ToString().Trim(),
                  Madre  = reader["madre"] == DBNull.Value ? "" : reader["madre"].ToString().Trim(),
                  Tipomov = tipomov,
                  FechaMov = reader["fechaMov"] == DBNull.Value ? "" : reader["fechaMov"].ToString().Trim(),
                  Numtran = reader["numtran"] == DBNull.Value ? "" : reader["numtran"].ToString().Trim(),
                  Pureza = reader["pureza"] == DBNull.Value ? "" : reader["pureza"].ToString().Trim(),
                  Et = reader["et"] == DBNull.Value ? "" : reader["et"].ToString().Trim(),
                  Sexog = reader["sexog"] == DBNull.Value ? "" : reader["sexog"].ToString().Trim(),
                  Capa = reader["capa"] == DBNull.Value ? "" : reader["capa"].ToString().Trim(),
                  Facpar = reader["facpar"] == DBNull.Value ? "" : reader["facpar"].ToString().Trim(),
                  Tamcr = reader["tamcr"] == DBNull.Value ? "" : reader["tamcr"].ToString().Trim(),
                  NumeroOrigen = reader["numero_Origen"] == DBNull.Value ? "" : reader["numero_Origen"].ToString().Trim(),
                  Cib = reader["cib"] == DBNull.Value ? "" : reader["cib"].ToString().Trim(),
                  Collar = reader["collar"] == DBNull.Value ? "" : reader["collar"].ToString().Trim(),
                  FechaDoc = reader["fechaDoc"] == DBNull.Value ? "" : reader["fechaDoc"].ToString().Trim(),
                  EsModif = reader["esModif"] == DBNull.Value ? "" : reader["esModif"].ToString().Trim()
                };
                ListaRegistrosConafe.Add(registro);
              }
            }
          }
        }
        var requestBody = new
        {
          animales = ListaRegistrosConafe
        };
        var json = JsonConvert.SerializeObject(requestBody);
        if (!Directory.Exists(directorioPayload))
        {
          Directory.CreateDirectory(directorioPayload);
        }

        string nombreFichero = "animales_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json";
        string rutaFichero = Path.Combine(directorioPayload, nombreFichero);
        // Guardar JSON
        File.WriteAllText(rutaFichero, json, Encoding.UTF8);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          using (SqlConnection connection = new SqlConnection(connectionString))
          {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("Reggen..SP_ConafeAPI_Animales", connection))
            {
              command.CommandType = CommandType.StoredProcedure;
              command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 1; // Marcar como enviados
              await command.ExecuteNonQueryAsync();
            }
          }
        }
        else
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API Animales: {response.StatusCode}, Detalles: {errorBody}");
        }

        string responseBody = await response.Content.ReadAsStringAsync();
        var respuesta = JsonConvert.DeserializeObject<RespuestaConafe>(responseBody);
        return respuesta ?? new RespuestaConafe();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new RespuestaConafe();
      }
    }

    public async Task<RespuestaConafe> GetCensoConafe()
    {
      string json = "";
      //string baseUrl = client.BaseAddress.ToString(); //https://sinbad.conafe.com/GESLIBSalidaPI
      string baseUrl = "https://sinbad.conafe.com/GESLIBSalidaAPI/censos/J";
      try
      {
        var response = await client.GetAsync(baseUrl).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          json = await response.Content.ReadAsStringAsync();
          var result = JsonConvert.DeserializeObject<List<CensoConafe>>(json);
          // Insertar registros en la tabla Reggen..CensoConafe
          if (result != null && result.Count > 0)
          {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              await connection.OpenAsync();
              // Primero, limpiar la tabla
              using (SqlCommand cmdDelete = new SqlCommand("DELETE FROM Reggen..CensoConafe", connection))
              {
                await cmdDelete.ExecuteNonQueryAsync();
              }

              // Insertar los registros
              string insertQuery = @"
                INSERT INTO Reggen..CensoConafe 
                  (numero, sexo, sexoge, capa, et, pureza, nombre, nupadre, numadre, ganacr, fnac, collar, ganapr, cib)
                VALUES 
                  (@numero, @sexo, @sexoge, @capa, @et, @pureza, @nombre, @nupadre, @numadre, @ganacr, @fnac, @collar, @ganapr, @cib)";

              foreach (var censo in result)
              {
                using (SqlCommand cmdInsert = new SqlCommand(insertQuery, connection))
                {
                  cmdInsert.Parameters.Add("@numero", SqlDbType.Char, 14).Value = censo.Numero ?? (object)DBNull.Value;
                  cmdInsert.Parameters.Add("@sexo", SqlDbType.Char, 1).Value = string.IsNullOrEmpty(censo.Sexo) ? (object)DBNull.Value : censo.Sexo;
                  cmdInsert.Parameters.Add("@sexoge", SqlDbType.Char, 1).Value = string.IsNullOrEmpty(censo.Sexoge) ? (object)DBNull.Value : censo.Sexoge;
                  cmdInsert.Parameters.Add("@capa", SqlDbType.VarChar, 10).Value = string.IsNullOrEmpty(censo.Capa) ? (object)DBNull.Value : censo.Capa;
                  cmdInsert.Parameters.Add("@et", SqlDbType.Char, 1).Value = string.IsNullOrEmpty(censo.Et) ? (object)DBNull.Value : censo.Et;
                  cmdInsert.Parameters.Add("@pureza", SqlDbType.Char, 2).Value = string.IsNullOrEmpty(censo.Pureza) ? (object)DBNull.Value : censo.Pureza;
                  cmdInsert.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = string.IsNullOrEmpty(censo.Nombre) ? (object)DBNull.Value : censo.Nombre;
                  cmdInsert.Parameters.Add("@nupadre", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(censo.Padre) ? (object)DBNull.Value : censo.Padre;
                  cmdInsert.Parameters.Add("@numadre", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(censo.Madre) ? (object)DBNull.Value : censo.Madre;
                  cmdInsert.Parameters.Add("@ganacr", SqlDbType.Char, 7).Value = string.IsNullOrEmpty(censo.Ganacr) ? (object)DBNull.Value : censo.Ganacr;

                  // Convertir fecha de string a DateTime
                  if (!string.IsNullOrEmpty(censo.Fnac) && DateTime.TryParse(censo.Fnac, out DateTime fnac))
                  {
                    cmdInsert.Parameters.Add("@fnac", SqlDbType.SmallDateTime).Value = fnac;
                  }
                  else
                  {
                    cmdInsert.Parameters.Add("@fnac", SqlDbType.SmallDateTime).Value = DBNull.Value;
                  }

                  cmdInsert.Parameters.Add("@collar", SqlDbType.VarChar, 10).Value = string.IsNullOrEmpty(censo.Collar) ? (object)DBNull.Value : censo.Collar;
                  cmdInsert.Parameters.Add("@ganapr", SqlDbType.Char, 7).Value = string.IsNullOrEmpty(censo.Ganapr) ? (object)DBNull.Value : censo.Ganapr;
                  cmdInsert.Parameters.Add("@cib", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(censo.Cib) ? (object)DBNull.Value : censo.Cib;

                  await cmdInsert.ExecuteNonQueryAsync();
                }
              }
            }

            return new RespuestaConafe 
            { 
              TotalRecibidos = result.Count,
              Insertados = result.Count,
              Rechazados = 0
            };
          }
        }
        else
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API Censo: {response.StatusCode}, Detalles: {errorBody}");
        }

        // Si no hay registros
        return new RespuestaConafe 
        { 
          TotalRecibidos = 0,
          Insertados = 0,
          Rechazados = 0
        };
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new RespuestaConafe();
      }
    }

    public async Task<RespuestaConafe> GetLactacionesConafe(string vaca)
    {
      string baseUrl = client.BaseAddress.ToString(); //https://sinbad.conafe.com/GESLIBEntradaAPI
      baseUrl = baseUrl + $"/lactaciones/lote";
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Lactaciones", connection))
          {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca ;
            command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 0;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var registro = new LactacionesConafe()
                {
                  Aut = reader["aut"] == DBNull.Value ? "" : reader["aut"].ToString().Trim(),
                  Numero = reader["numero"] == DBNull.Value ? "" : reader["numero"].ToString().Trim(),
                  Numpar = reader["numpar"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["numpar"]),
                  Ganlact = reader["ganlact"] == DBNull.Value ? "" : reader["ganlact"].ToString().Trim(),
                  Dias = reader["dias"] == DBNull.Value ? (short)0 : Convert.ToInt16(reader["dias"]),
                  Klnat = reader["klnat"] == DBNull.Value ? 0 : Convert.ToInt32(reader["klnat"]),
                  Kgnat = reader["kgnat"] == DBNull.Value ? (short)0 : Convert.ToInt16(reader["kgnat"]),
                  Kpnat = reader["kpnat"] == DBNull.Value ? (short)0 : Convert.ToInt16(reader["kpnat"]),
                  Kl305 = reader["kl305"] == DBNull.Value ? 0 : Convert.ToInt32(reader["kl305"]),
                  Kg305 = reader["kg305"] == DBNull.Value ? (short)0 : Convert.ToInt16(reader["kg305"]),
                  Kp305 = reader["kp305"] == DBNull.Value ? (short)0 : Convert.ToInt16(reader["kp305"]),
                  Fincurso = reader["fincurso"] == DBNull.Value ? "" : reader["fincurso"].ToString().Trim()
                };
                if (registro.Fincurso == "F")
                {
                  registro.Fbaj = reader["fbaj"] == DBNull.Value ? "" : Convert.ToDateTime(reader["fbaj"]).ToString("yyyy-MM-dd");

                  registro.Tipobaja = reader["tipobaja"] == DBNull.Value ? (byte) 0  : Convert.ToByte(reader["tipobaja"]);
                }
                ListaLactacionesConafe.Add(registro);
              }
            }
          }
        }
        var requestBody = new
        {
          lactaciones = ListaLactacionesConafe
        };
        var json = JsonConvert.SerializeObject(requestBody);
        // Leer directorio desde App.config
        if (!Directory.Exists(directorioPayload))
        {
          Directory.CreateDirectory(directorioPayload);
        }
        // Nombre del fichero: lactaciones_20260531_1435.json
        string nombreFichero = "lactaciones_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json";
        string rutaFichero = Path.Combine(directorioPayload, nombreFichero);
        // Guardar JSON
        File.WriteAllText(rutaFichero, json, Encoding.UTF8);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          using (SqlConnection connection = new SqlConnection(connectionString))
          {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Lactaciones", connection))
            {
              command.CommandType = CommandType.StoredProcedure;
              command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
              command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 1; // Marcar como enviados
              await command.ExecuteNonQueryAsync();
            }
          }
        }
        else
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API Lactaciones: {response.StatusCode}, Detalles: {errorBody}");
        }

        string responseBody = await response.Content.ReadAsStringAsync();
        var respuesta = JsonConvert.DeserializeObject<RespuestaConafe>(responseBody);
        return respuesta ?? new RespuestaConafe();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new RespuestaConafe();
      }
    }
    public async Task<RespuestaConafe> GetControlesConafe(string vaca)
    { object alfa  ;
      string baseUrl = client.BaseAddress.ToString(); //https://sinbad.conafe.com/GESLIBEntradaAPI
      baseUrl = baseUrl + $"/controles/lote";
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Controles", connection))
          {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
            command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 0;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var registro = new ControlesConafe()
                {
                  Aut = reader["aut"] == DBNull.Value ? "" : reader["aut"].ToString().Trim(),
                  Numero = reader["numero"] == DBNull.Value ? "" : reader["numero"].ToString().Trim(),
                  Numpar = reader["numpar"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["numpar"]),
                  Fcontrol = reader["fcontrol"] == DBNull.Value ? "" : Convert.ToDateTime(reader["fcontrol"]).ToString("yyyy-MM-dd"),
                  Metodo = reader["metodo"] == DBNull.Value || string.IsNullOrWhiteSpace(reader["metodo"].ToString()) ? "1M" : reader["metodo"].ToString().Trim(),
                  Leche = reader["leche"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["leche"]),
                  Grasa = reader["grasa"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["grasa"]),
                  Proteina = reader["proteina"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["proteina"]),
                  Rcs = reader["rcs"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rcs"]),
                  Lactosa = reader["lactosa"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["lactosa"]),
                  Urea = reader["urea"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["urea"]),
                  Bhb = reader["bhb"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["bhb"]),
                  Acetona = reader["acetona"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["acetona"]),
                  Fana = reader["fana"] == DBNull.Value ? "" : Convert.ToDateTime(reader["fana"]).ToString("yyyy-MM-dd"),
                 Controlador = reader["controlador"] == DBNull.Value ? 0 : Convert.ToInt64(reader["controlador"]),
                 Ganlact = reader["ganlact"] == DBNull.Value ? "" : reader["ganlact"].ToString().Trim()
                };
                if (reader["incidencia"] != DBNull.Value && !string.IsNullOrEmpty(reader["incidencia"].ToString().Trim()))
                {
                  alfa= reader["incidencia"];
                  registro.Incidencia = Convert.ToByte(reader["incidencia"].ToString().Trim());
                }
                 
                ListaControlesConafe.Add(registro);
              }
            }
          }
        }
        var requestBody = new
        {
          controles = ListaControlesConafe // Enviar en lotes de 1000 registros para evitar problemas de tamaño
        };
        var json = JsonConvert.SerializeObject(requestBody);
        if (!Directory.Exists(directorioPayload))
        {
          Directory.CreateDirectory(directorioPayload);
        }
        string nombreFichero = "controles_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json";
        string rutaFichero = Path.Combine(directorioPayload, nombreFichero);
        File.WriteAllText(rutaFichero, json, Encoding.UTF8);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          using (SqlConnection connection = new SqlConnection(connectionString))
          {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Controles", connection))
            {
              command.CommandType = CommandType.StoredProcedure;
              command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
              command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 1; // Marcar como enviados
              await command.ExecuteNonQueryAsync();
            }
          }
        }
        else
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API controles: {response.StatusCode}, Detalles: {errorBody}");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        var respuesta = JsonConvert.DeserializeObject<RespuestaConafe>(responseBody);
        return respuesta ?? new RespuestaConafe();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new RespuestaConafe();
      }
    }
    public async Task<RespuestaConafe> GetPartosConafe(string vaca)
    {
      string baseUrl = client.BaseAddress.ToString(); //https://sinbad.conafe.com/GESLIBEntradaWS
      baseUrl = baseUrl + $"/partos/lote";
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Partos", connection))
          {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
            command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 0;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var registro = new PartosConafe()
                {
                  Aut = reader["aut"] == DBNull.Value ? "" : reader["aut"].ToString().Trim(),
                  Numero = reader["numero"] == DBNull.Value ? "" : reader["numero"].ToString().Trim(),
                  Numpar = reader["numpar"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["numpar"]),
                  Fpar = reader["fpar"] == DBNull.Value ? "" : Convert.ToDateTime(reader["fpar"]).ToString("yyyy-MM-dd"),
                  Facpar = reader["facpar"] == DBNull.Value ? (byte)0 : 
                    byte.TryParse(
                      reader["facpar"].ToString().Trim(), out byte facpar)  
                    ? facpar  : (byte)0,

                  Tamcria = reader["tamcria"] == DBNull.Value ? (byte)0 :
                    byte.TryParse(
                      reader["tamcria"].ToString().Trim(),
                      out byte tamcria) ? tamcria : (byte)0,
                  Retplacenta = reader["retplacenta"] == DBNull.Value ? "" : reader["retplacenta"].ToString().Trim(),
                  Ganpar = reader["ganpar"] == DBNull.Value ? "" : reader["ganpar"].ToString().Trim()
                };
                ListaPartosConafe.Add(registro);
              }
            }
          }
        }
        var requestBody = new
        {
          partos = ListaPartosConafe
        };
        var json = JsonConvert.SerializeObject(requestBody);
        // Leer directorio desde App.config
        if (!Directory.Exists(directorioPayload))
        {
          Directory.CreateDirectory(directorioPayload);
        }
        // Nombre del fichero: partos_20260531_1435.json
        string nombreFichero = "partos_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json";
        string rutaFichero = Path.Combine(directorioPayload, nombreFichero);
        // Guardar JSON
        File.WriteAllText(rutaFichero, json, Encoding.UTF8);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content).ConfigureAwait(false);
        
          if (response.IsSuccessStatusCode)
          {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              await connection.OpenAsync();
              using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Partos", connection))
              {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
                command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 1; // Marcar como enviados
                await command.ExecuteNonQueryAsync();
              }
            }
          }
        else
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API Partos: {response.StatusCode}, Detalles: {errorBody}");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        var respuesta = JsonConvert.DeserializeObject<RespuestaConafe>(responseBody);
        return respuesta ?? new RespuestaConafe();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new RespuestaConafe();
      }
    }
    public async Task<RespuestaConafe> GetCubricionesConafe(string vaca)
    {
      string baseUrl = client.BaseAddress.ToString(); 
      baseUrl = baseUrl + $"/cubriciones/lote";
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Cubriciones", connection))
          {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
            command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 0;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var registro = new CubricionesConafe()
                {
                  Aut = reader["aut"] == DBNull.Value ? "" : reader["aut"].ToString().Trim(),
                  Hembra = reader["hembra"] == DBNull.Value ? "" : reader["hembra"].ToString().Trim(),
                  Fecha = reader["fecha"] == DBNull.Value ? "" : Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                  Tipo = reader["tipo"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["tipo"]),
                //  Diagnostico = reader["diagnostico"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["diagnostico"]),
                //  Fdiag = reader["fdiag"] == DBNull.Value ? "" : Convert.ToDateTime(reader["fdiag"]).ToString("yyyy-MM-dd"),
                  Ganaderia = reader["ganaderia"] == DBNull.Value ? "" : reader["ganaderia"].ToString().Trim(),
                  Inseminador = reader["inseminador"] == DBNull.Value ? "" : reader["inseminador"].ToString().Trim(),
                  Macho = reader["macho"] == DBNull.Value ? "" : reader["macho"].ToString().Trim(),
                 // MetodoDiagnostico = reader["metodoDiagnostico"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["metodoDiagnostico"]),
                  //Marca = reader["marca"] == DBNull.Value ? (byte)0 : Convert.ToByte(reader["marca"])
                };
                ListaCubricionesConafe.Add(registro);
              }
            }
          }
        }
        var requestBody = new
        {
          cubriciones = ListaCubricionesConafe
        };
        var json = JsonConvert.SerializeObject(requestBody);

        if (!Directory.Exists(directorioPayload))
        {
          Directory.CreateDirectory(directorioPayload);
        }
        string nombreFichero = "cubriciones_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json";
        string rutaFichero = Path.Combine(directorioPayload, nombreFichero);
        // Guardar JSON
        File.WriteAllText(rutaFichero, json, Encoding.UTF8);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
          using (SqlConnection connection = new SqlConnection(connectionString))
          {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SP_ConafeAPI_Cubriciones", connection))
            {
              command.CommandType = CommandType.StoredProcedure;
              command.Parameters.Add("@vaca", SqlDbType.Char, 14).Value = string.IsNullOrEmpty(vaca) ? (object)DBNull.Value : vaca;
              command.Parameters.Add("@updateregs", SqlDbType.Bit).Value = 1; // Marcar como enviados
              await command.ExecuteNonQueryAsync();
            }
          }
        }
        else
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API Cubriciones: {response.StatusCode}, Detalles: {errorBody}");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        var respuesta = JsonConvert.DeserializeObject<RespuestaConafe>(responseBody);

      

        return respuesta ?? new RespuestaConafe();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new RespuestaConafe();
      }
    }
 
  }
}
