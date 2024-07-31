namespace Api.AppCore.API_BrasilAPI.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class DddBrasilApi
    {
        #region --> Public properties. <--

        public string ddd { get; set; }

        public string state { get; set; }

        public List<string> cities { get; set; }

        #endregion --> Public properties. <--
    }
}
