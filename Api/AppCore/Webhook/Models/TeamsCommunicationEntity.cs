namespace Api.AppCore.Webhook.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TeamsCommunicationEntity
    {
        #region --> Public properties. <--

        public string type { get; set; }

        public string text { get; set; }

        public TeamsCommunicationMentioned mentioned { get; set; }

        #endregion --> Public properties. <--
    }
}
