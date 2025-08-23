using Afca.Common;
using Afca.Common.Models;
using Afca.Common.ServicesInterfaces;
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
    public async Task<List<MuestraCruzada>> GetMuestrasCruzadas()
    {
      if (string.IsNullOrEmpty(FechaHasta))
      {
        FechaHasta = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
      }
      if (string.IsNullOrEmpty(FechaDesde))
      {
        FechaDesde = DateTime.Now.AddDays(-10).ToString("dd-MM-yyyy");
      }
      FechaDesde = FechaDesde.Replace("/", "-");
      FechaHasta = FechaHasta.Replace("/", "-");

      //1.Me conecto a la BD de CL para ver las muestras de analitica sin cruzar
      //creo el JSON para enviar a la API
      try
      {
        connectionString = ConfigurationManager.ConnectionStrings["controllechero"]?.ConnectionString;

        string query = $"SELECT TOP 1 * FROM control where res_fprev between '{FechaDesde}' and '{FechaHasta}' and res_ab='a' and res_trc=0";
        string query_fake = $"SELECT TOP 1 * FROM control where res_ord='69990000000001'";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          await connection.OpenAsync();
          using (SqlCommand command = new SqlCommand(query_fake, connection))
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

      //string baseUrl = client.BaseAddress.ToString(); //https://resapi.lilc.es:3081/api/
      //baseUrl = baseUrl + $"INSControllechero/cruzarmuestras/";//{FechaDesde}/{FechaHasta}";
      //comentar para local
     
      try
      {
        string baseUrl = "https://localhost:44347/api/controllechero/cruzarmuestras";
         
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
  }
}
