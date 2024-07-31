namespace Api.AppCore.API_BrasilAPI.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ZipCodeLocationBrasilApi
    {
        #region --> Public properties. <--

        public string type { get; set; }

        public ZipCodeCoordinatesBrasilApi coordinates { get; set; }

        #endregion --> Public properties. <--
    }
}
