using Api.AppCore.API_BrasilAPI.Controllers;
using Api.AppCore.API_BrasilAPI.Models;
using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.API_ReceitaWsApi.Controllers;
using Api.AppCore.API_ReceitaWsApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 22/07/2024 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class LegalPersonController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public LegalPersonController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        /// <summary>
        /// Obtenção de dados de pessoa jurídica de acordo com o CNPJ informado.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../legalperson/get
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpGet]
        [Route("get")]
        public ServiceResponse<CommonLegalPerson> Get([Required] string cnpj)
        {
            ServiceResponse<CommonLegalPerson> serviceResponse = new();

            try
            {
                if (cnpj == default || !Validation.IsCnpj(cnpj)) serviceResponse.message = "É necessário informar o CNPJ válido.";
                else
                {
                    ReceitaWsApi receitaWsApi = new();
                    ServiceResponse<LegalPersonReceitaWsApi> serviceResponseLegalPersonReceitaWsApi = receitaWsApi.GetLegalPerson(cnpj);

                    if (serviceResponseLegalPersonReceitaWsApi.success)
                    {
                        serviceResponse.obj = new(serviceResponseLegalPersonReceitaWsApi.obj);
                        serviceResponse.success = serviceResponseLegalPersonReceitaWsApi.success;
                        serviceResponse.message = serviceResponseLegalPersonReceitaWsApi.message;
                    }
                    else
                    {
                        BrasilApi brasilApi = new();
                        ServiceResponse<LegalPersonBrasilApi> serviceResponseLegalPersonBrasilApi = brasilApi.GetLegalPerson(cnpj);

                        if (serviceResponseLegalPersonBrasilApi.success)
                        {
                            serviceResponse.obj = new(serviceResponseLegalPersonBrasilApi.obj);
                            serviceResponse.success = serviceResponseLegalPersonBrasilApi.success;
                            serviceResponse.message = serviceResponseLegalPersonBrasilApi.message;
                        }
                    }

                    if (!serviceResponse.success) serviceResponse.message = "Não foi possível localizar pessoa jurídica com o CNPJ informado.";
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
