using Afca.Common;
using Afca.Common.DataAccessor;
using Afca.Common.Models;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Data.Access
{
    public class AgmWebApiDataAccessor : IAgmWebApiDataAccessor
    {
        string loginController = "user";
        string dataHubController = "datahub";

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


            var response = await ServerRepositoryHttpClient()
                                .PutAsync(urlAction
                                 , new StringContent(parameter, Encoding.UTF8, "application/json"))
                            .ConfigureAwait(false);

            bool responseOk = HandleResponse(response);

            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return new Result {success=responseOk
                             ,value=responseOk?"":content};
        }
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
            LoadWebServiceUrl();
            LoadWebApiCredentials();
        }
        void LoadWebServiceUrl()
        {
            //load from app.config (encrypted saved)

            var url = ConfigurationManager.AppSettings["AgmWebApiUrl"];

            if (url != null)
                urlWebAPI = Encriptador.Decrypt(url,false);
            else
                throw new Exception("Conexión con el Web API AGM no encontrada");

            //urlWebAPI = @"http://localhost/AGMWebAPI/api";
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
                //throw exception and loggoff
                //ex = new Exception(RGlobal.HTTP_401_Message)
                //{
                //    InternalMessage = RGlobal.HTTP_401_Internal
                //};
                return false;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
                //throw new vException(RGlobal.HTTP_404_Message)
                //{
                //    InternalMessage = RGlobal.HTTP_404_Internal
                //};
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return false;
                //throw new vException(RGlobal.HTTP_403_Message)
                //{
                //    InternalMessage = RGlobal.HTTP_403_Internal
                //};
            }

            if (response.StatusCode == System.Net.HttpStatusCode.RequestEntityTooLarge)
            {
                return false;
                //throw new vException(RGlobal.HTTP_413_Message)
                //{
                //    InternalMessage = RGlobal.HTTP_413_Internal
                //};
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return false;
                //throw new vException(RGlobal.HTTP_413_Message)
                //{
                //    InternalMessage = RGlobal.HTTP_413_Internal
                //};
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK
                    && response.StatusCode != System.Net.HttpStatusCode.Accepted
                    && response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                //throw new vException(RGlobal.HTTP_Other)
                //{
                //    InternalMessage = response.StatusCode.ToString()
                //};
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
