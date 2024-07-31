using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.SISS_Integration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 26/11/2020 - Luiz Lenire. <-- */

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public sealed class AuthenticationController
    {
        #region --> Private properties. <--      

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public AuthenticationController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--    

        #region --> Public methods. <--      

        /// <summary>
        /// Método para gerar o token de acesso aos métodos da API.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///{
        ///"Username":"usertest",
        ///"Password":"passtest"
        ///}
        /// </remarks>
        /// <returns>Retornará um token para ser informado em cada método da API, sem ele, o acesso será negado.</returns>
        /// <response code="200">Token gerado com sucesso.</response>
        /// <response code="400">Usuário/senha inválidos.</response>     
        ///       
        [HttpPost]
        [Route("token/generate")]
        public ActionResult<dynamic> GenerateToken(ConnectLogin connectLogin)
        {
            ServiceResponse<string> serviceResponse = new();

            try
            {
                string message = default;
                TokenService tokenService = new();

                connectLogin = tokenService.Get(connectLogin.Username, connectLogin.Password, ref message);

                if (message != default) serviceResponse.message = message;
                else
                {
                    serviceResponse.obj = tokenService.GenerateToken(connectLogin);
                    serviceResponse.success = true;
                    serviceResponse.message = "Token gerado com sucesso.";
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
