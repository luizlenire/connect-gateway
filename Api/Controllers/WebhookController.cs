using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.Webhook.Controllers;
using Api.AppCore.Webhook.SeveralFunctions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 23/02/2023 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class WebhookController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public WebhookController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--  

        #region --> Public methods. <--    

        [HttpPost]
        [Route("microsoft-teams/send")]
        public ServiceResponse<string> MicrosoftTeamsSend([Required] EnumTeamsCommunication.Chat chat, [FromBody] string message)
        {
            ServiceResponse<string> serviceResponse = new();

            try
            {
                TeamsCommunication teamsCommunication = new();
                serviceResponse.obj = teamsCommunication.Send(chat, message);

                if (serviceResponse.obj == default)
                {
                    serviceResponse.success = true;
                    serviceResponse.message = "Mensagem enviada ao Microsoft Teams com sucesso.";
                }
                else serviceResponse.message = "Não foi possível enviar esta mensagem ao Microsoft Teams.";
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
