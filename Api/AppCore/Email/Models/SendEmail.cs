namespace Api.AppCore.Email.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class SendEmail
    {
        #region --> Public properties. <--

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Recipient { get; set; }

        #endregion --> Public properties. <--
    }
}
