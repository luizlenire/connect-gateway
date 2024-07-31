using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.API_Regulamentacao.Controllers;
using Api.AppCore.API_Regulamentacao.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Api.Controllers
{
    /* --> † 24/11/2020 - Luiz Lenire. <-- */

#if (DEBUG)
    [AllowAnonymous]
#endif

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = roles, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class RegulamentacaoController
    {
        #region --> Private properties. <--

        private const string roles = TokenService.Siss;

        private Stopwatch stopwatch { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public RegulamentacaoController()
        {
            stopwatch = new();
            stopwatch.Start();
        }

        #endregion --> Constructors. <--    

        #region --> Public methods. <--

        [HttpGet]
        [Route("get")]
        public ServiceResponse<List<ProfissionalBuscaSimples>> Get([Required] string orgaoEmissor, [Required] string uf, [Required] string busca, [Required] string extensao)
        {
            ServiceResponse<List<ProfissionalBuscaSimples>> serviceResponse = new();

            try
            {
                orgaoEmissor = orgaoEmissor != default ? orgaoEmissor.ToUpper() : orgaoEmissor;
                uf = uf != default ? uf.ToUpper() : uf;
                extensao = extensao != default ? extensao.ToLower() : extensao;

                if (!Regulamentacao.listOrgaoEmissor.Contains(orgaoEmissor)) serviceResponse.message = "O órgão emissor informado não está mapeado.";
                else if (!Regulamentacao.listUF.Contains(uf)) serviceResponse.message = "O UF informado não está mapeado.";
                else if (!Regulamentacao.listExtensions.Contains(extensao)) serviceResponse.message = "A extensão informada não está mapeado.";
                else
                {
                    Regulamentacao regulamentacao = new(orgaoEmissor, uf, busca, extensao);
                    serviceResponse = regulamentacao.Get();

                    if (serviceResponse.obj != default)
                    {
                        serviceResponse.success = true;
                        serviceResponse.message = "Registro localizado com sucesso.";
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
