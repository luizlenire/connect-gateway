using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.API_SmartSms.Controllers;
using Api.AppCore.API_SmartSms.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 04/02/2021 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class SmsController
    {
        #region --> Private properties. <--      

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public SmsController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--  

        #region --> Public methods. <--    

        [HttpPost]
        [Route("message/send")]
        public ServiceResponse<string> SendMessage(CommonSms commonSms)
        {
            ServiceResponse<string> serviceResponse = new();

            try
            {
                commonSms.phone = Masking.RemoveAllNonNumeric(commonSms.phone);

                if (commonSms.phone == default) serviceResponse.message = "É necessário informar um telefone válido.";
                else if (commonSms.message == default) serviceResponse.message = "É necessário informar uma mensagem válida.";
                else
                {

                    SmartSms smartSms = new();
                    ServiceResponseSmartSms serviceResponseSmartSms = smartSms.Send(commonSms.phone, commonSms.message);

                    if (serviceResponseSmartSms.success)
                    {
                        serviceResponse.success = serviceResponseSmartSms.success;
                        serviceResponse.message = "Mensagem enviada com sucesso.";
                    }
                    else serviceResponse.message = serviceResponseSmartSms.message;
                }
            }
            catch (Exception ex) { serviceResponse = Startup.responseAPI.ProcessException(serviceResponse, ex); }
            finally { serviceResponse.message += Tools.GlobalFinally(serviceResponse, stopwatch); }

            return serviceResponse;
        }

        #endregion --> Public methods. <--
    }
}
