using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.API_Regulamentacao.Models;
using Newtonsoft.Json;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace Api.AppCore.API_Regulamentacao.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Regulamentacao
    {
        #region --> Private properties. <--

        private string UrlRequest { get; set; }

        #endregion --> Private properties. <--

        #region --> Public static properties. <--

        public static readonly List<string> listOrgaoEmissor = new() { "CRM", "CRO", "CREA", "CFC", "CREF", "OAB", "CRP", "CRF", "CAU" };

        public static readonly List<string> listUF = new() { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

        public static readonly List<string> listExtensions = new() { "xml", "json" };

        #endregion --> Public static properties. <--

        #region --> Constructors. <--

        public Regulamentacao(string orgaoEmissor, string uf, string busca, string extensao)
        {
            UrlRequest = GlobalAtributtes.UrlRegulamentacao;
            UrlRequest += "?tipo=" + orgaoEmissor;
            UrlRequest += "&uf=" + uf;
            UrlRequest += "&q=" + busca;
            UrlRequest += "&chave=" + GlobalAtributtes.ChaveRegulamentacao;
            UrlRequest += "&destino=" + extensao;
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        public ServiceResponse<List<ProfissionalBuscaSimples>> Get()
        {
            ServiceResponse<List<ProfissionalBuscaSimples>> serviceResponse = new();

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(UrlRequest, default).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                ProfissionalBusca profissionalBusca = default;

                if (UrlRequest.Split("&destino=")[1].Equals("json")) profissionalBusca = JsonConvert.DeserializeObject<ProfissionalBusca>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                else if (UrlRequest.Split("&destino=")[1].Equals("xml"))
                {
                    using XmlReader reader = XmlReader.Create(new StringReader(httpResponseMessage.Content.ReadAsStringAsync().Result));
                    profissionalBusca = ((ProfissionalBuscaXml)(new XmlSerializer(typeof(ProfissionalBuscaXml))).Deserialize(reader)).profissionalBusca;
                }

                if (profissionalBusca.item.Count != default)
                {
                    serviceResponse.obj = new();
                    foreach (ProfissionalBuscaDetalhe item in profissionalBusca.item) serviceResponse.obj.Add(new(item));
                }
            }
            else serviceResponse.message = "StatusCode: " + httpResponseMessage.StatusCode + ", " + httpResponseMessage.Content.ReadAsStringAsync().Result;

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
