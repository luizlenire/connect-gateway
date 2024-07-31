using Newtonsoft.Json;

namespace Api.AppCore.Webhook.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TeamsCommunicationContent
    {
        #region --> Public properties. <--

        public string type { get; set; }

        public List<TeamsCommunicationBody> body { get; set; }

        [JsonProperty("$schema")]
        public string schema { get; set; }

        public string version { get; set; }

        public TeamsCommunicationMsteams msteams { get; set; }

        #endregion --> Public properties. <--
    }
}
