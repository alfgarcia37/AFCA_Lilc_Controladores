using Afca.Common;
using Afca.Common.DataAccessor;
using Afca.Common.DataSets;
using Afca.Common.Models;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Data.Access
{
    public class AgmWebApiDataAccessor : IAgmWebApiDataAccessor
    {
        string loginController = "user";
        string dataHubController = "datahub";
        string RCSController = "RCS";
        string NacimientoController = "Nacimiento";

        //load from app.config (encrypted saved)
        string email = "";
        string key = "";

        #region agm web api data accessor methods
        public AgmWebApiDataAccessor()
        {
            InitializeHttpClient();
        }
        public async Task<Result> UploadData(string ganaderiaId, string jsonData)
        {
            var setRoutine = await SetConnectionContext();

            if (!setRoutine.success) 
                return new Result { success = false
                                   , value = setRoutine.value };

            var urlAction = UrlWebAPIController(dataHubController);
            urlAction = string.Format("{0}{1}", urlAction, ganaderiaId);

            var parameter = JsonConvert.ToString(jsonData);

            //var alfa = new StringContent(parameter, Encoding.UTF8, "application/json");

            //cuando estoy dandole caña con SP's y subidas, peta por time out
            // ServerRepositoryHttpClient().Timeout = TimeSpan.FromSeconds(60);


            var response = await ServerRepositoryHttpClient()
                                .PutAsync(urlAction
                                 , new StringContent(parameter, Encoding.UTF8, "application/json"))
                            .ConfigureAwait(false);

            bool responseOk = HandleResponse(response);

            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return new Result {success=responseOk
                             ,value=responseOk?"":content};
        }
        public async Task<Result> ExecuteIndicadores()
        {
            var setRoutine = await SetConnectionContext();

            if (!setRoutine.success)
                return new Result
                {
                    success = false,
                    value = setRoutine.value
                };
           

            var urlAction = UrlWebAPIController(dataHubController);

            urlAction = string.Concat(urlAction, "Runbook");
            var parameter = JsonConvert.ToString("body");
            var response = await ServerRepositoryHttpClient().PostAsync(urlAction, new StringContent(parameter, Encoding.UTF8, "application/json"))
                          .ConfigureAwait(false);

            bool responseOk = HandleResponse(response);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Result>(json);

            return result ?? null;
        }

        public async Task<Result> ExecuteNotificacionesFirebase()
        {
          var setRoutine = await SetConnectionContext();

          if (!setRoutine.success)
            return new Result
            {
              success = false
                               ,
              value = setRoutine.value
            };

            var urlAction = UrlWebAPIController(dataHubController);
            urlAction = string.Concat(urlAction, "FirebaseToken");
            urlAction = "http://localhost/AGMWebAPI/api/Datahub/FirebaseToken";
            var response = await ServerRepositoryHttpClient().GetAsync(urlAction).ConfigureAwait(false);

            bool responseOk = HandleResponse(response);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Result>(json);
       
            return result;
    }

        public async Task<Result> DownloadRCSData()
        {

            var setRoutine = await SetConnectionContext();

            if (!setRoutine.success)
                return new Result
                {
                    success = false
                                   ,
                    value = setRoutine.value
                };

            var urlAction = UrlWebAPIController(RCSController);
            urlAction = string.Concat(urlAction,"EnvioRCS" );

            var response = await ServerRepositoryHttpClient()
                                .GetAsync(urlAction)
                                .ConfigureAwait(false);

            return new Result{
                success=true,
                value = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            };
        }
        public async Task<Result> DownloadNacimientoData()
        {

            var setRoutine = await SetConnectionContext();

            if (!setRoutine.success)
                return new Result
                {
                    success = false
                                   ,
                    value = setRoutine.value
                };

            var urlAction = UrlWebAPIController(NacimientoController);
            urlAction = string.Concat(urlAction, "EnvioNacimiento");

            var response = await ServerRepositoryHttpClient()
                                .GetAsync(urlAction)
                                .ConfigureAwait(false);

            return new Result
            {
                success = true,
                value = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            };
        }
        #region descargas
        public async Task<AgmDownloadData> DownloadAgmData(string entidad)
        {
            DateTime start = DateTime.Now;
            AgmDownloadData data = new AgmDownloadData();
            DataRow mappedDataRow = data.EntidadData.NewRow();

            switch (entidad.ToUpper())
            {
                case "RCS":
                    var rcsDownloadOperation = await DownloadRCSData();
                    //var jsonRcsMessage = "{ entidad:'RCS',fechaDescarga:'2023-05-27',data:[{rcs:1},{rcs:2}]}";
                    if(rcsDownloadOperation.success)
                    {
                        data.EntidadData.Rows.Add(
                            MapDownloadedMessageToDataRow(entidad, rcsDownloadOperation.value, start, DateTime.Now, mappedDataRow)
                      );
                    }
                  
                    break;
                case "NACIMIENTO":
                    var nacimientoDownloadOperation = await DownloadNacimientoData();
                    if (nacimientoDownloadOperation.success)
                    {
                        data.EntidadData.Rows.Add(MapDownloadedMessageToDataRow(entidad, nacimientoDownloadOperation.value, start, DateTime.Now, mappedDataRow)                      );
                    }

                    break;
            }
            return data;
        }

        public async Task<Result> NotifyDescargaAsRecieved(string jsondata, string entidad = null)
        {
            //var parameter = JsonConvert.SerializeObject(jsondata);
            var jsonDataStringified = JsonConvert.ToString(jsondata);
            //ahora ya tengo

            var setRoutine = await SetConnectionContext();

            if (!setRoutine.success)
                return new Result
                {
                    success = false
                                   ,
                    value = setRoutine.value
                };
           
            var urlAction = UrlWebAPIController(dataHubController);
            urlAction = string.Format("{0}Confirmacion", urlAction);

            //var parameterprueba = "{\"RCS\":[{\"AsociacionID\":1,\"CodigoBarras\":\"69990024017999\",\"Nombre\":\"mixta\"},{\"AsociacionID\":1,\"GanaderiaID\":\"J390950   \",\"CodigoBarras\":\"69990024026913\",\"Nombre\":\"careta                        \",\"FechaDescarga\":\"2015-06-03T00:00:00\",\"FechaEntregaControlador\":\"2023-04-25T00:00:00\",\"FechaEntregaGanadero\":\"2015-06-01T00:00:00\",\"FechaAnalisis\":\"2023-04-26T00:00:00\",\"Celulas\":432,\"ControladorID\":24,\"FechaRegistro\":\"2023-06-01T13:27:58.860\",\"EstadoBuzon\":\"PDTE\"}]}";
            //parameterprueba = JsonConvert.ToString(parameterprueba);

            //var alfa = new StringContent(parameter, Encoding.UTF8, "application/json");

            var response = await ServerRepositoryHttpClient()
                                .PutAsync(urlAction
                                 , new StringContent(jsonDataStringified, Encoding.UTF8, "application/json"))
                                .ConfigureAwait(false);
            
            bool responseOk = HandleResponse(response);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Result>(json);

            //var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //var result = JsonConvert.DeserializeObject<Result>(json);
            //return result;

            //var res= new Result() { success = false, value = "" };
            return result;
        }
        DataRow MapDownloadedMessageToDataRow(string entidad,string jsonMessage,DateTime startDateTime,DateTime endDateTime,DataRow row)
        {
          

            //devolver del servidor un json con la siguiente estructura:
            /*
             * {
             *   entidad:'RCS',
             *   FechaDescarga:2023-5-20,
             *   data:[{}] <- array de entidades
             * 
             * }
             * */
            DownloadedData jsonData = JsonConvert.DeserializeObject<DownloadedData>(jsonMessage);
            row["AsociacionID"] = 1;
            row["Entidad"] = entidad;
            row["InicioDescarga"] = startDateTime;
            row["FinDescarga"] = endDateTime;
            row["FechaDescarga"] = jsonData.FechaDescarga;
            row["Message"] = JsonConvert.SerializeObject(jsonData.Data);

            return row;
        }
        #endregion

        #endregion

        #region http common interface methods
        readonly HttpClient httpClient = new HttpClient();
        string urlWebAPI = "";

        HttpClient ServerRepositoryHttpClient() => httpClient;
        string UrlWebAPI() => urlWebAPI;
        static LoginToken Token;

        void InitializeHttpClient()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(1800);
            LoadWebServiceUrl();
            LoadWebApiCredentials();
        }
        void LoadWebServiceUrl()
        {
            //load from app.config (encrypted saved)

            var url = ConfigurationManager.AppSettings["AgmWebApiUrl"];

            if (url != null)
                urlWebAPI = Encriptador.Decrypt(url, false);
            else
                throw new Exception("Conexión con el Web API AGM no encontrada");
        }
        void LoadWebApiCredentials()
        {
            var credentials = ConfigurationManager.AppSettings["AgmWebApiUser"];

            if (credentials != null)
            {
                string credentialsRaw = Encriptador.Decrypt(credentials,false);
                string[] parts = credentialsRaw.Split(new char[] {';' });

                if (parts.Length == 2)
                {
                    email = parts[0];
                    key = parts[1];
                    return;
                }

                throw new Exception("Credenciales con el Web API AGM no encontradas");

            }
            else
                throw new Exception("Credenciales con el Web API AGM no encontradas");
        }
        string UrlWebAPIController(string controller)
        {
            return string.Concat(UrlWebAPI(), string.Format("/{0}/", controller));
        }
        void SetAuthorizationHeader()
        {
            if (Token != null && !Token.IsExpired())
            {
                if (!ServerRepositoryHttpClient().DefaultRequestHeaders.Contains("Authorization"))
                    ServerRepositoryHttpClient().DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", Token.AuthToken));
            }
        }
        bool HandleResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                
                return false;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
               
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return false;
                
            }

            if (response.StatusCode == System.Net.HttpStatusCode.RequestEntityTooLarge)
            {
                return false;
               
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return false;
               
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK
                    && response.StatusCode != System.Net.HttpStatusCode.Accepted
                    && response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                
                return false;
            }
            return true;
        }

        #endregion

        #region http web api login methods
        async Task<Result> SetConnectionContext()
        {
            if (Token != null && !Token.IsExpired())
            {
                SetAuthorizationHeader();
                return new Result { success = true };
            }
            else
            {
                var loginResponse = await DoLogin(email, key);

                if (loginResponse.Success && loginResponse.AuthToken != null)
                {
                    Token = new LoginToken(key, loginResponse.AuthToken);
                    Token.CreatedAt = DateTime.Now;
                    SetAuthorizationHeader();
                    return new Result { success = true };
                }
                else
                    return new Result { success = false, value = loginResponse.Message };
            }
        }
        public async Task<LoginResponse> DoLogin(string email, string key)
        {
            var urlAction = UrlWebAPIController(loginController);

            var loginInfo = new { Email = email, Pass = key };
            var parameter = JsonConvert.SerializeObject(loginInfo);


            var response = await ServerRepositoryHttpClient()
                                .PostAsync(urlAction
                                 , new StringContent(parameter, Encoding.UTF8, "application/json"))
                            .ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<LoginResponse>(json);
            return result;
        }
        #endregion


    }
}
