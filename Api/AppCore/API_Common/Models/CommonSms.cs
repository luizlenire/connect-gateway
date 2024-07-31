using System.ComponentModel.DataAnnotations;

namespace Api.AppCore.API_Common.Models
{
    /* --> † 3/07/2024 - Luiz Lenire. <-- */

    public sealed class CommonSms
    {
        #region --> Public properties. <--

        [Required]
        public string phone { get; set; }

        [Required]
        public string message { get; set; }

        #endregion --> Public properties. <--
    }
}
