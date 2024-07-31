using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.Email.Models;
using Api.AppCore.Webhook.SeveralFunctions;
using Api.Controllers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Api
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Startup
    {
        #region --> Public static properties. <--

        public static ResponseApi responseAPI = new();

        public const string _ApplicationName = "Connect API | Gateway";

        public const string _Version = "1.0";

        private static string _DashboardTitle
        {
            get
            {
                if (Debugger.IsAttached)
                {
                    return "1 - Debugger | " + _ApplicationName + " " + _Version + " | Rise Date: " + Tools.GetDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss");
                }
                else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    return "2 - Development | " + _ApplicationName + " " + _Version + " | Rise Date: " + Tools.GetDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss");
                }
                else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    return "4 - Production | " + _ApplicationName + " " + _Version + " | Rise Date: " + Tools.GetDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss");
                }
                else return default;
            }
        }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public Startup()
        {
            IConfiguration iconfiguration = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).Build();

            GlobalAtributtes.UrlAwesome = iconfiguration["UrlAwesome"];
            GlobalAtributtes.UrlBrasilApi = iconfiguration["UrlBrasilApi"];
            GlobalAtributtes.UrlCode7ApiSms = iconfiguration["UrlCode7ApiSms"];
            GlobalAtributtes.UserNameCode7ApiSms = iconfiguration["UserNameCode7ApiSms"];
            GlobalAtributtes.PasswordCode7ApiSms = iconfiguration["PasswordCode7ApiSms"];
            GlobalAtributtes.UrlCode7ApiWhatsapp = iconfiguration["UrlCode7ApiWhatsapp"];
            GlobalAtributtes.UserNameCode7ApiWhatsapp = iconfiguration["UserNameCode7ApiWhatsapp"];
            GlobalAtributtes.PasswordCode7ApiWhatsapp = iconfiguration["PasswordCode7ApiWhatsapp"];
            GlobalAtributtes.UrlRegulamentacao = iconfiguration["UrlRegulamentacao"];
            GlobalAtributtes.ChaveRegulamentacao = iconfiguration["ChaveRegulamentacao"];
            GlobalAtributtes.UrlSmartSms = iconfiguration["UrlSmartSms"];

            ////Implementar estes 3 recursos abaixo
            //BrasilApi brasilApi = new();
            //ServiceResponse<List<HolidaysBrasilApi>> listServiceResponseHolidaysBrasilApi = brasilApi.GetHolidays(Tools.GetDateTimeNow());
            //ServiceResponse<List<IbgeCodeCityBrasilApi>> listServiceResponseIbgeCodeCityBrasilApi = brasilApi.GetIbgeCodeCity("PE");
            ////ServiceResponse<List<AdditionalInfoUfBrasilApi>> listServiceResponseAdditionalInfoUfBrasilApi = brasilApi.GetAdditionalInfoUf("PE");        

            //return;
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        public void ConfigureServices(IServiceCollection iServiceCollection)
        {
            iServiceCollection.AddControllers();
            iServiceCollection.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            AuthenticationJwT();
            Swagger();

            //† 08/03/2024 - Luiz Lenire. - Comentado healthcheck pois no container nao esta funcionando.
            //HealthChecks();

            #region --> Sub-methods. <--

            void AuthenticationJwT()
            {
                iServiceCollection.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenService.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            }

            void Swagger()
            {
                iServiceCollection.AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new()
                    {
                        Title = _DashboardTitle,
                        Version = _Version,
                        Description = "API para interfaceamento de recursos externos.",
                        Contact = new()
                        {
                            Name = "Luiz Lenire",
                            Url = default,
                            Email = "luizlenire@outlook.com",
                        },

                    });
                    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                });
            }

            void HealthChecks()
            {
                iServiceCollection.AddHealthChecks();
                iServiceCollection.AddHealthChecks().AddUrlGroup(new Uri(GlobalAtributtes.UrlRegulamentacao), "UrlRegulamentacao");
                iServiceCollection.AddHealthChecks().AddUrlGroup(new Uri(GlobalAtributtes.UrlCode7ApiSms), "UrlCode7ApiSms");
                iServiceCollection.AddHealthChecks().AddUrlGroup(new Uri(GlobalAtributtes.UrlCode7ApiWhatsapp), "UrlCode7ApiWhatsapp");

                iServiceCollection.AddHealthChecksUI(options =>
                {
                    options.SetEvaluationTimeInSeconds(5);
                    options.MaximumHistoryEntriesPerEndpoint(10);
                    options.AddHealthCheckEndpoint("API com Health Checks", "/health");
                }).AddInMemoryStorage();
            }

            #endregion --> Sub-methods. <--
        }

        public void Configure(IApplicationBuilder iApplicationBuilder, IWebHostEnvironment iWebHostEnvironment)
        {
            if (iWebHostEnvironment.IsDevelopment()) iApplicationBuilder.UseDeveloperExceptionPage();

            iApplicationBuilder.UseHttpsRedirection();
            iApplicationBuilder.UseRouting();
            iApplicationBuilder.UseAuthorization();
            iApplicationBuilder.UseAuthentication();
            iApplicationBuilder.UseEndpoints(x => x.MapControllers());

            iApplicationBuilder.UseCors(x => x.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());

            Swagger();
            Redocs();

            //† 08/03/2024 - Luiz Lenire. - Comentado healthcheck pois no container nao esta funcionando.
            //HealthChecks();

            NewVersion();

            #region --> Sub-methods. <--

            void Swagger()
            {
                iApplicationBuilder.UseSwagger();
                iApplicationBuilder.UseSwaggerUI(x => x.SwaggerEndpoint($"{(string.IsNullOrWhiteSpace(x.RoutePrefix) ? "." : "..")}/swagger/v1/swagger.json", _ApplicationName));
            }

            void Redocs()
            {
                iApplicationBuilder.UseReDoc(c =>
                {
                    c.DocumentTitle = _ApplicationName;
                    c.SpecUrl = "/swagger/v1/swagger.json";
                });
            }

            void HealthChecks()
            {
                iApplicationBuilder.UseHealthChecks("/health-checks", new HealthCheckOptions
                {
                    Predicate = p => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                iApplicationBuilder.UseHealthChecksUI(options => { options.UIPath = "/health-checks-dashboard"; });
            }

            void NewVersion()
            {
                EmailController emailController = new();

                emailController.Send(new SendEmail
                {
                    Subject = "Nova versão " + _DashboardTitle,
                    Body = "Olá, detectada uma nova subida de versão do " + _DashboardTitle + ".",
                    Recipient = "luizlenire@outlook.com"
                });

                if (!Debugger.IsAttached)
                {
                    WebhookController webhookController = new();
                    webhookController.MicrosoftTeamsSend(EnumTeamsCommunication.Chat.DevelopmentChat, "Olá, detectada uma nova subida de versão do " + _DashboardTitle + ".");
                }
            }

            #endregion --> Sub-methods. <--
        }

        #endregion --> Public methods. <--
    }
}
