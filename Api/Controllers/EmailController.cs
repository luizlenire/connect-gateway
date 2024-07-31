using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.Email.Controllers;
using Api.AppCore.Email.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 07/01/2022 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmailController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public EmailController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--  

        #region --> Public methods. <--

        /// <summary>
        /// Método responsável por disparar todos os e-mails de origem do Siss Connect.
        /// </summary>
        /// <remarks>
        /// Exemplo: ../email/send
        /// {
        ///    "Subject": "Saúde CFM - Código de autenticação",
        ///    "Body": "Segue o seu código de autenticação para acesso ao aplicativo: 57144 Este é um e-mail automático, favor não responder!",
        ///    "Recipient": "luiz.filho@tecnogroup.com.br"
        /// }
        /// </remarks>
        /// <returns>Objeto contendo um detalhamento dos dados inseridos, um objeto sucesso e um objeto com uma mensagem com o descritivo de toda a operação realizada.</returns>
        /// <response code="200">Retorno com sucesso, no objeto de retorno, informações referente a toda a operação.</response>
        /// <response code="400">Em caso de reprovação, retornará um objeto descrevendo qual foi o motivo da reprovação.</response>   
        [HttpPost]
        [Route("send")]
        public ServiceResponse<string> Send([FromBody] SendEmail sendEmail)
        {
            ServiceResponse<string> serviceResponse = new();

            try
            {
                if (sendEmail.Subject == default) serviceResponse.message = "É necessário informar um assunto.";
                else if (sendEmail.Body == default) serviceResponse.message = "É necessário informar uma mensagem.";
                else if (sendEmail.Recipient == default) serviceResponse.message = "É necessário informar um ou mais destinatários (ex: joao@gmail.com ou joao@gmail.com;jose@gmail.com).";
                else if (!Validation.IsEmail(sendEmail.Recipient)) serviceResponse.message = "É necessário informar um e-mail válido.";
                else
                {
                    Email email = new();
                    email.Send(sendEmail);

                    serviceResponse.success = true;
                    serviceResponse.message = "Mensagem enviada com sucesso.";
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
