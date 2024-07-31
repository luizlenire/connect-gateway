using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.Webhook.Models;
using Api.AppCore.Webhook.SeveralFunctions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Api.AppCore.Webhook.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TeamsCommunication
    {
        #region --> Private properties. <--

        private const string DevelopmentChat = @"";

        #endregion --> Private properties. <--

        #region --> Public methods. <--

        public string Send(EnumTeamsCommunication.Chat chat, string message)
        {
            string url = default;
            string mensagemInicial = "Olá, ";
            DateTime dateRef = Tools.GetDateTimeNow();

            if (dateRef.Hour > 6 && dateRef.Hour < 12) mensagemInicial += "bom dia!";
            else if (dateRef.Hour >= 12 && dateRef.Hour < 18) mensagemInicial += "boa tarde!";
            else mensagemInicial += "boa noite!";

            mensagemInicial += " " + dateRef.ToString("dd/MM/yyyy HH:mm:ss");

            TeamsCommunicationRoot teamsCommunicationRoot = new()
            {
                type = "message",
                attachments = new List<TeamsCommunicationAttachment>()
                {
                    new TeamsCommunicationAttachment()
                    {
                        contentType = "application/vnd.microsoft.card.adaptive",
                        content = new TeamsCommunicationContent()
                        {
                           type = "AdaptiveCard",
                           body = new List<TeamsCommunicationBody>()
                           {
                               new TeamsCommunicationBody()
                               {
                                   type = "TextBlock",
                                   size = "Medium",
                                   weight = "Bolder",
                                   text = mensagemInicial
                               },
                               new TeamsCommunicationBody()
                               {
                                   type = "TextBlock"
                               },
                               new TeamsCommunicationBody()
                               {
                                   type = "TextBlock",
                                   text = "Por favor, analisem a mensagem abaixo."
                               }
                           },
                           schema = "http://adaptivecards.io/schemas/adaptive-card.json",
                           version = "1.0",
                           msteams = new TeamsCommunicationMsteams()
                           {
                               entities = new List<TeamsCommunicationEntity>()
                           }
                        }
                    }
                }
            };

            Collaborator collaborator = new();

            if (chat == EnumTeamsCommunication.Chat.DevelopmentChat)
            {
                url = DevelopmentChat;

                teamsCommunicationRoot.attachments[0].content.body[1].text = "<at>" + collaborator.LuizLenire.Name + "</at>";

                teamsCommunicationRoot.attachments[0].content.msteams.entities.Add
                (
                    new TeamsCommunicationEntity()
                    {
                        type = "mention",
                        text = "<at>" + collaborator.LuizLenire.Name + "</at>",
                        mentioned = new TeamsCommunicationMentioned()
                        {
                            id = collaborator.LuizLenire.Email,
                            name = collaborator.LuizLenire.Name
                        }
                    }
                );
            }

            using HttpClient httpClient = new();

            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url,
                                                                           new StringContent(JsonConvert.SerializeObject(teamsCommunicationRoot),
                                                                           Encoding.UTF8,
                                                                           "application/json")).Result;

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                httpResponseMessage = httpClient.PostAsync(url,
                                                           new StringContent(JsonConvert.SerializeObject(new
                                                           {
                                                               context = "https://schema.org/extensions",
                                                               type = "MessageCard",
                                                               text = message
                                                           }),
                                                           Encoding.UTF8,
                                                           "application/json")).Result;

                return default;
            }
            else return httpResponseMessage.Content.ReadAsStringAsync().Result;
        }

        #endregion --> Public methods. <--
    }
}
