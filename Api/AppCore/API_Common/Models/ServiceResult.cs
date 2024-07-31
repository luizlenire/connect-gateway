namespace Api.AppCore.API_Common.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public abstract class ServiceResult
    {
        #region --> Public properties. <--

        public bool success { get; set; }

        public string message { get; set; }

        #endregion --> Public properties. <--
    }
}
