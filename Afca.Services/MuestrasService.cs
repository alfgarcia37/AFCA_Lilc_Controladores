using Afca.Common;
using Afca.Common.Models;
using Afca.Common.ServicesInterfaces;
using Afca.Common.Utilities;
using FrameworkForge.Common;
using Newtonsoft.Json; // Asegúrate de tener este using
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Afca.Services
{
  public class MuestrasService : IMuestrasService
  {
    public HttpClient client= new HttpClient();
    public List<Muestra> ListaMuestras = new List<Muestra>();
    public List<RegistroConafe> ListaRegistrosConafe = new List<RegistroConafe>();
    public List<MuestraIndustria> Listaindustria = new List<MuestraIndustria>();
    public string FechaDesde { get; set; }
    public string FechaHasta { get; set; }
    string connectionString = ConfigurationManager.ConnectionStrings["controllechero"].ConnectionString;
    // connectionString ="https://localhost:44347/api/"

    string usuario = "Lilc@1900";
    string pass = "Xjk1_H23#34pjvKS";

    public MuestrasService()
    {

#if DEBUG
      ServicePointManager.ServerCertificateValidationCallback =
          new RemoteCertificateValidationCallback((sender, cert, chain, errors) => true); // SOLO DEV
#endif
      client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiConfigPath"]); //"https://resapi.lilc.es:3081/api/" 
     
      client.DefaultRequestHeaders.Accept.Clear();
      FechaDesde = ConfigurationManager.AppSettings["FechaDesde"];
      FechaHasta = ConfigurationManager.AppSettings["FechaHasta"];
    }

    // MUESTRAS DE CONTROL LECHERO CRUZADAS
    public async Task<List<MuestraCruzada>> GetMuestrasCruzadas(string establo)
    {
      if (ListaMuestras.Count > 0)
        ListaMuestras.Clear();
      if (string.IsNullOrEmpty(FechaHasta))
      {
        FechaHasta = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy");
      }
      //if (string.IsNullOrEmpty(FechaDesde))
      {
        FechaDesde = DateTime.Now.AddDays(-15).ToString("dd-MM-yyyy");
      }
      if (establo.Trim()!="")
        FechaDesde = DateTime.Now.AddDays(-25).ToString("dd-MM-yyyy");

      FechaDesde = FechaDesde.Replace("/", "-");
      FechaHasta = FechaHasta.Replace("/", "-");

      //1.Me conecto a la BD de CL para ver las muestras de analitica sin cruzar
      //creo el JSON para enviar a la API
      try
      {
        connectionString = ConfigurationManager.ConnectionStrings["controllechero"]?.ConnectionString;
        // dentro de un mes , quitar and res_trc=0 pr res_factu ya tendra valor
        string query = $@"SELECT TOP 2500 * FROM control where res_fctr between '{FechaDesde}' and '{FechaHasta}' 
          and res_ab='a' and res_trc=0 and res_factu is null    
          and (res_tit='{establo}' or '{establo}'='')  
          order by res_ord";
        string query_fake = $"SELECT TOP 1 * FROM control where res_ord='69990008019011' order by res_fprev desc";// and res_fprev between '{FechaDesde}' and '{FechaHasta}'";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand(query, connection))
          {
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var codigo = reader["Res_ord"]?.ToString();
                if (codigo != null)
                {
                  var registro = new Muestra(codigo)
                  {
                    //Codbarras = codigo.Trim(),
                    FechaRegistro = DateTime.Now.Date.ToString("yyyy/MM/dd"),
                    Establo = reader["Res_tit"]?.ToString(),
                    NombreControlador = reader["Controlador"]?.ToString(),
                    Recombi = "1",
                    FechaControlador = Convert.ToDateTime(reader["Res_Fctr"]).ToString("yyyy/MM/dd"),
                    Ptecombi = "1"
                  };
                  ListaMuestras.Add(registro);
                }
              }
            }
          }
        }

        //ahora meto las de RCS individuales.
        query = $@"SELECT TOP 50 * FROM RCS  where FRegistroRcs between '{FechaDesde}' and '{FechaHasta}' 
        and FechaCargaAzure is null";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand(query, connection))
          {
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
              while (await reader.ReadAsync())
              {
                var codigo = reader["Barras"]?.ToString();
                if (codigo != null)
                {
                  var registro = new Muestra(codigo)
                  {
                    FechaRegistro = DateTime.Now.Date.ToString("yyyy/MM/dd"),
                    Establo = reader["ESTABLO"]?.ToString(),
                    NombreControlador = reader["Controlador"]?.ToString(),
                    Recombi = "1",
                    FechaControlador = Convert.ToDateTime(reader["FEntregaControlador"]).ToString("yyyy/MM/dd"),
                    Ptecombi = "1"
                  };
                  ListaMuestras.Add(registro);
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        var e = ex.Message;
        return new List<MuestraCruzada>(); // Retorna una lista vacía en caso de error
      }
      //Encabezadso con Basic authenticator. Construye el string de autenticación en Base64
      string authString = $"{usuario}:{pass}";
      string base64Auth = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authString));
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);

      string baseUrl = client.BaseAddress.ToString(); //https://resapi.lilc.es:3083/api/
      baseUrl = baseUrl + $"/controllechero/cruzarmuestras";//{FechaDesde}/{FechaHasta}";
      //baseUrl = "http://localhost:33947/api/Controllechero/cruzarmuestras/";
      try
      {
        var json = JsonConvert.SerializeObject(ListaMuestras);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content).ConfigureAwait(false);
        //var test = "https://agmwebapi.azurewebsites.net/api/values"; // Test URL for debugging
        //var responsetest = await client.GetAsync(test).ConfigureAwait(false);
        //string body = await responsetest.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API: {response.StatusCode}, Detalles: {errorBody}");
        }
        // Si todo va bien, leer contenido
        var result = response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //var response = await client.PostAsJsonAsync(baseUrl, ListaMuestras);
        string responseBody = await response.Content.ReadAsStringAsync();
        List<MuestraCruzada> listaCruzada = JsonConvert.DeserializeObject<List<MuestraCruzada>>(responseBody);
        return listaCruzada ?? new List<MuestraCruzada>();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      return new List<MuestraCruzada>();
    }


    // MUESTRAS INDUSTRIA DIARIAS
    public async Task<List<MuestraIndustria>> GetMuestrasIndustria(string Establo,DateTime? FDesde,DateTime? FHasta)
    {
      if (Listaindustria.Count > 0)
        Listaindustria.Clear(); 
      string authString = $"{usuario}:{pass}";
      string base64Auth = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authString));
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);

      string baseUrl = client.BaseAddress.ToString(); //https://resapi.lilc.es:3083/api/
      if (FDesde.HasValue && FHasta.HasValue)
      {
        FechaDesde = FDesde.Value.ToString("dd-MM-yyyy").Replace("/", "-");
        FechaHasta = FHasta.Value.ToString("dd-MM-yyyy").Replace("/", "-");
        baseUrl = baseUrl + $"/Industria/GetMuestrasIndustria/{Establo}/{FechaDesde}/{FechaHasta}";
      }
      else
      {
        baseUrl = baseUrl + $"/Industria/GetMuestrasIndustria";
      }
      try
      {
        var response = await client.GetAsync(baseUrl).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API: {response.StatusCode}, Detalles: {errorBody}");
        }
        // Si todo va bien, leer contenido
        var result = response.Content.ReadAsStringAsync().ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        Listaindustria = JsonConvert.DeserializeObject<List<MuestraIndustria>>(responseBody);
        return Listaindustria ?? new List<MuestraIndustria>();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      return new List<MuestraIndustria>();
    }


    //MEDIAS DE INDUSTRIA MENSUALES
    public async Task<List<MuestraIndustria>> GetMediasIndustria(string establo, string mes, string ano)
    {

      string authString = $"{usuario}:{pass}";
      string base64Auth = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authString));
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Auth);

      string baseUrl = client.BaseAddress.ToString(); //https://resapi.lilc.es:3083/api/
      if (!string.IsNullOrEmpty(mes) && !string.IsNullOrEmpty(ano))
        baseUrl = baseUrl + $"/Industria/GetMediasIndustria/{establo}/{mes}/{ano}";
      else
        baseUrl = baseUrl + $"/Industria/GetMediasIndustria";
     

      try
      {

        var response = await client.GetAsync(baseUrl).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          throw new HttpRequestException($"Error en la llamada API: {response.StatusCode}, Detalles: {errorBody}");
        }
        // Si todo va bien, leer contenido
        var result = response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //var response = await client.PostAsJsonAsync(baseUrl, ListaMuestras);
        string responseBody = await response.Content.ReadAsStringAsync();
        List<MuestraIndustria> listaindustria = JsonConvert.DeserializeObject<List<MuestraIndustria>>(responseBody);
        return listaindustria ?? new List<MuestraIndustria>();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      return new List<MuestraIndustria>();
    }

   

  }
}
