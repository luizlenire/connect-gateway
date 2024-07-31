using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Api.AppCore.API_Common.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public abstract class RequestResponse
    {
        #region --> Private properties. <--

        private const string MessageSuccess = "Informações obtidas com sucesso.";

        private const string MessageFailure = "Não foi possivel obter informações.";

        private const string MessageException = "Ocorreu uma falha grave ao consultar as informações, segue mais detalhes: ";

        #endregion --> Private properties. <--

        #region --> Private methods. <--

        protected dynamic Get(string url, string param, Type type)
        {
            using HttpClient httpClient = new();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url + param).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK) return JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result, type);
            else return default;
        }

        protected dynamic GetAuthenticated(string url, string param, Type type, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url + param).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK) return JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result, type);
            else return default;
        }

        protected dynamic Process(dynamic serviceResponse)
        {
            try
            {
                if (serviceResponse.obj != default(dynamic))
                {
                    serviceResponse.success = true;
                    serviceResponse.message = MessageSuccess;
                }
                else serviceResponse.message = MessageFailure;
            }
            catch (Exception ex) { serviceResponse.message = MessageException + ex.Message; }

            return serviceResponse;
        }

        #endregion --> Private methods. <--
    }
}
