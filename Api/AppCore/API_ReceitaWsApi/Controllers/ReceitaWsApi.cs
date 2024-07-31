using Api.AppCore.API_Common.Controllers;
using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.API_ReceitaWsApi.Models;

namespace Api.AppCore.API_ReceitaWsApi.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ReceitaWsApi : RequestResponse
    {
        #region --> Public methods. <--     

        public ServiceResponse<LegalPersonReceitaWsApi> GetLegalPerson(string cnpj)
        {
            ServiceResponse<LegalPersonReceitaWsApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlReceitaWs, cnpj, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
