namespace Api.AppCore.API_Common.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ServiceResponse<T> : ServiceResult
    {
        #region --> Public properties. <--

        public T obj { get; set; }

        #endregion --> Public properties. <--
    }
}