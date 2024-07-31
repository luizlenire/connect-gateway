namespace Api.AppCore.API_AwesomeApi.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class AddressAwesomeApi
    {
        #region --> Public properties <--

        public string cep { get; set; }

        public string address_type { get; set; }

        public string address_name { get; set; }

        public string address { get; set; }

        public string state { get; set; }

        public string district { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        public string city { get; set; }

        public string city_ibge { get; set; }

        public string ddd { get; set; }

        #endregion --> Public properties. <--
    }
}
