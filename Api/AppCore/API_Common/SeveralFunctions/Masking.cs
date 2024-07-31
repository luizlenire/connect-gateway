using System.Text.RegularExpressions;

namespace Api.AppCore.API_Common.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Masking
    {
        #region --> Public static methods. <--

        public static string RemoveAllNonNumeric(string value)
        {
            if (value == default) return value;
            else
            {
                Regex regex = new(@"[^\d]");
                return regex.Replace(value, "");
            }
        }

        public static string UndoCnpj(string value)
        {
            if (value != default)
            {
                value = value.Trim();
                value = value.Replace(".", default);
                value = value.Replace("-", default);
                value = value.Replace("/", default);
                value = value.Replace(" ", default);
            }

            return value;
        }

        public static string UndoCpf(string value)
        {
            if (value != default)
            {
                value = value.Trim();
                value = value.Replace(".", default);
                value = value.Replace("-", default);
                value = value.Replace(" ", default);
            }

            return value;
        }

        #endregion --> Public static methods. <--
    }
}
