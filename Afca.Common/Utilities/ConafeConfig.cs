using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Utilities
{ 
  public static class Constants
    {
      public static string MiTokenLelyGlobal = "";
      public static string user = "sg1246";
      public static string pass = "NF3s9SG8YAxn$Kg3";
      public static string hashedPassword = StringUtils.ComputeSHA256Hash(pass);
    }
 
 
    public static class ConafeConfig
    {
    //private static string site_url = "https://sinbad.conafe.com/WebApi_AMSOS360/";
    private static string site_url = ConfigurationManager.AppSettings["ApiConfigPathConafe"];// "https://sinbad.conafe.com/WebApi_AMSOS360/login";

    public static void SetAuthorizationHeaderConafe(HttpClient httpClient, string token)
        {
          {
            GetToken(httpClient);
            token = Constants.MiTokenLelyGlobal;
          }

          if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
          {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
          }
          else
          {
            httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
          }
        }

      public static void GetToken(HttpClient httpClient)
        {
          Constants.MiTokenLelyGlobal = "";
          string UrlWebAPIconafe= site_url + "/login";
          var body = new
          {
            usuario = Constants.user,
            hashPwd = Constants.hashedPassword
          };
          string json = JsonConvert.SerializeObject(body);
          var content = new StringContent(json, Encoding.UTF8, "application/json");

          var response = httpClient.PostAsync(UrlWebAPIconafe, content).GetAwaiter().GetResult();
          if (!response.IsSuccessStatusCode)
          {
            var errorBody = response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException($"Error en la llamada API Lactaciones: {response.StatusCode}, Detalles: {errorBody}");
          }

          var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
          JObject mijson = JObject.Parse(jsonResponse);
          var lelyToken = mijson["token"].ToString();
          Constants.MiTokenLelyGlobal = lelyToken;
        }

    }
}
