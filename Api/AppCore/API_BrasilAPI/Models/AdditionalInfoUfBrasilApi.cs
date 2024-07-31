namespace Api.AppCore.API_BrasilAPI.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class AdditionalInfoUfBrasilApi
    {
        #region --> Public properties. <--

        public int id { get; set; }

        public string sigla { get; set; }

        public string nome { get; set; }

        public AdditionalInfoUfRegiaoBrasilApi regiao { get; set; }

        #endregion --> Public properties. <--
    }
}
