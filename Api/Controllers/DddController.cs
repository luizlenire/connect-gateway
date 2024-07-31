using Api.AppCore.API_BrasilAPI.Controllers;
using Api.AppCore.API_BrasilAPI.Models;
using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public sealed class DddController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        private List<string> listDdd = new(new string[]
       {
           "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
           "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
           "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
           "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
           "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
           "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
           "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
           "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
           "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
           "90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
       });

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public DddController()
        {
            stopwatch = new();
            stopwatch.Start();
        }
        #endregion --> Constructors. <--

        #region --> Public methods. <--

        /// <summary>
        /// Obtenção de dados de cidades de acordo com o DDD informado.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../ddd/get
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpGet]
        [Route("get")]
        public ServiceResponse<DddBrasilApi> Get(string code)
        {
            ServiceResponse<DddBrasilApi> serviceResponse = new();

            try
            {
                BrasilApi brasilApi = new();
                serviceResponse = brasilApi.GetDdd(code);

                if (serviceResponse != default && serviceResponse.obj != default) serviceResponse.obj.ddd = code;
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        /// <summary>
        /// Obtenção de dados de cidades relacionadas aos DDD's disponíveis.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../ddd/get-list
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpGet]
        [Route("get-list")]
        public ServiceResponse<List<DddBrasilApi>> Get()
        {
            ServiceResponse<List<DddBrasilApi>> serviceResponse = new();

            try
            {
                BrasilApi brasilApi = new();

                foreach (string item in listDdd)
                {
                    ServiceResponse<DddBrasilApi> serviceResponseAux = brasilApi.GetDdd(item);

                    if (serviceResponseAux != default &&
                        serviceResponseAux.obj != default &&
                        serviceResponseAux.obj.cities != default &&
                        serviceResponseAux.obj.cities.Any())
                    {
                        if (serviceResponse.obj == default)
                        {
                            serviceResponse.obj = [];
                            serviceResponse.success = serviceResponseAux.success;
                            serviceResponse.message = serviceResponseAux.message;
                        }

                        if (serviceResponseAux != default && serviceResponseAux.obj != default) serviceResponseAux.obj.ddd = item;

                        serviceResponse.obj.Add(serviceResponseAux.obj);
                    }
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}