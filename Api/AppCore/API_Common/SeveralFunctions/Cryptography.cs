using System.Security.Cryptography;
using System.Text;

namespace Api.AppCore.API_Common.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Cryptography
    {
        #region --> Public methods. <--

        public static string DoMD5(string value)
        {
            try
            {
                using MD5 md5 = MD5.Create();
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder stringBuilder = new();

                for (int i = 0; i < data.Length; i++) stringBuilder.Append(data[i].ToString("x2"));

                return stringBuilder.ToString();
            }
            catch { return value; }
        }

        #endregion --> Public methods. <--
    }
}