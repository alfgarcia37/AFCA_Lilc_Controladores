using Afca.Common;
using Afca.Common.DataAccessor;
using Afca.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        string email = "fernando@gmail.com";
        string key = "Pepona123";

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
        }
        void LoadWebServiceUrl()
        {
            //load from app.config (encrypted saved)
            urlWebAPI = @"http://localhost/AGMWebAPI/api";
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
