namespace Api.AppCore.Webhook.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TeamsCommunicationBody
    {
        #region --> Public properties. <--

        public string type { get; set; }

        public string size { get; set; }

        public string weight { get; set; }

        public string text { get; set; }

        #endregion --> Public properties. <--
    }
}
