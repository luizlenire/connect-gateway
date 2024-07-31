using Api.AppCore.API_AwesomeApi.Models;
using Api.AppCore.API_Common.Controllers;
using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;

namespace Api.AppCore.API_AewsomeApi.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class AwesomeApi : RequestResponse
    {
        #region --> Public methods. <--

        public ServiceResponse<AddressAwesomeApi> Get(string cep)
        {
            ServiceResponse<AddressAwesomeApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlAwesome, cep, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
