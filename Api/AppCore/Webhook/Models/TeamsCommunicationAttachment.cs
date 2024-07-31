namespace Api.AppCore.Webhook.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TeamsCommunicationAttachment
    {
        #region --> Public properties. <--

        public string contentType { get; set; }

        public TeamsCommunicationContent content { get; set; }

        #endregion --> Public properties. <--
    }
}
