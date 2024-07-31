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
    /* --> † 23/07/2024 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class BankController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public BankController()
        {
            stopwatch = new();
            stopwatch.Start();
        }
        #endregion --> Constructors. <--

        #region --> Public methods. <--

        /// <summary>
        /// Obtenção de dados de banco de acordo com o código informado.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../bank/get
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpGet]
        [Route("get")]
        public ServiceResponse<BankBrasilApi> Get([Required] string code)
        {
            ServiceResponse<BankBrasilApi> serviceResponse = new();

            try
            {
                BrasilApi brasilApi = new();
                serviceResponse = brasilApi.GetBank(code);
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        /// <summary>
        /// Obtenção de lista de dados de bancos.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../bank/get-list
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpGet]
        [Route("get-list")]
        public ServiceResponse<List<BankBrasilApi>> Get()
        {
            ServiceResponse<List<BankBrasilApi>> serviceResponse = new();

            try
            {
                BrasilApi brasilApi = new();
                serviceResponse = brasilApi.GetBanks();

                if (serviceResponse.obj != default &&
                    serviceResponse.obj.Any())
                {
                    serviceResponse.obj = serviceResponse.obj.Where(x => x.name != default).ToList();
                    serviceResponse.obj = serviceResponse.obj.Where(x => x.fullName != default).ToList();

                    serviceResponse.obj.ForEach(x => { x.name = (x.name != default ? x.name.ToUpper() : x.name); x.fullName = (x.fullName != default ? x.fullName.ToUpper() : x.fullName); });
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
