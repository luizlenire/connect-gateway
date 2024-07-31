namespace Api.AppCore.API_BrasilAPI.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ZipCodeBrasilApi
    {
        #region --> Public properties. <--

        public string cep { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string neighborhood { get; set; }

        public string street { get; set; }

        public string service { get; set; }

        public ZipCodeLocationBrasilApi location { get; set; }

        #endregion --> Public properties. <--
    }
}
