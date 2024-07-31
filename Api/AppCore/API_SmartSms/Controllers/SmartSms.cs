using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.API_SmartSms.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Api.AppCore.API_SmartSms.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class SmartSms
    {
        public ServiceResponseSmartSms Send(string number, string message)
        {
            ServiceResponseSmartSms serviceResponseSmartSms = new();

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("ApiKey", "");

            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(GlobalAtributtes.UrlSmartSms,
                                                                           new StringContent(JsonConvert.SerializeObject(new
                                                                           {
                                                                               celular = number,
                                                                               mensagem = message,
                                                                               parceiroId = "824",
                                                                               carteiraId = 0
                                                                           }),
                                                                          Encoding.UTF8,
                                                                          "application/json")).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                serviceResponseSmartSms = JsonConvert.DeserializeObject<ServiceResponseSmartSms>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                if (serviceResponseSmartSms.message == "Message sent successfully")
                {
                    serviceResponseSmartSms.success = true;
                    serviceResponseSmartSms.message = "Mensagem enviada com sucesso com sucesso.";
                }
            }
            else serviceResponseSmartSms.message = "Ocorreu uma falha grave ao enviar a mensagem. | " + httpResponseMessage.Content.ReadAsStringAsync().Result;

            return serviceResponseSmartSms;
        }
    }
}
