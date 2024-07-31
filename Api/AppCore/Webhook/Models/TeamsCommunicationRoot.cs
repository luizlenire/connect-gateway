namespace Api.AppCore.Webhook.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TeamsCommunicationRoot
    {
        #region --> Public properties. <--

        public string type { get; set; }

        public List<TeamsCommunicationAttachment> attachments { get; set; }

        #endregion --> Public properties. <--
    }
}
