using Api.AppCore.API_AewsomeApi.Controllers;
using Api.AppCore.API_AwesomeApi.Models;
using Api.AppCore.API_BrasilAPI.Controllers;
using Api.AppCore.API_BrasilAPI.Models;
using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 26/11/2020 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class CepController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public CepController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        /// <summary>
        /// Obtenção de dados de endereço de acordo com o CEP informado, em caso de maior precisão, poderá ser informado o número.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../cep/get
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpGet]
        [Route("get")]
        public ServiceResponse<CommonZipCode> Get([Required] string cep, string numero)
        {
            ServiceResponse<CommonZipCode> serviceResponse = new();

            try
            {
                cep = Masking.RemoveAllNonNumeric(cep);

                if (cep == default || !Validation.IsCEP(cep)) serviceResponse.message = "É necessário informar o CEP válido.";
                else
                {
                    AwesomeApi awesomeApi = new();
                    ServiceResponse<AddressAwesomeApi> serviceResponseAddressAwesomeApi = awesomeApi.Get(cep);

                    if (serviceResponseAddressAwesomeApi.success)
                    {
                        serviceResponse.obj = new(serviceResponseAddressAwesomeApi.obj);
                        serviceResponse.success = serviceResponseAddressAwesomeApi.success;
                        serviceResponse.message = serviceResponseAddressAwesomeApi.message;
                    }
                    else
                    {
                        BrasilApi brasilApi = new();
                        ServiceResponse<ZipCodeBrasilApi> serviceResponseZipCodeBrasilApi = brasilApi.GetZipCodeV2(cep);

                        if (serviceResponseZipCodeBrasilApi.success)
                        {
                            serviceResponse.obj = new(serviceResponseZipCodeBrasilApi.obj);
                            serviceResponse.success = serviceResponseZipCodeBrasilApi.success;
                            serviceResponse.message = serviceResponseZipCodeBrasilApi.message;
                        }
                        else
                        {
                            serviceResponseZipCodeBrasilApi = brasilApi.GetZipCodeV1(cep);

                            if (serviceResponseZipCodeBrasilApi.success)
                            {
                                serviceResponse.obj = new(serviceResponseZipCodeBrasilApi.obj);
                                serviceResponse.success = serviceResponseZipCodeBrasilApi.success;
                                serviceResponse.message = serviceResponseZipCodeBrasilApi.message;
                            }
                        }
                    }

                    if (!serviceResponse.success) serviceResponse.message = "Não foi possível localizar dados de endereço com o CEP informado.";
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
