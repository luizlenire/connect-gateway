using Newtonsoft.Json;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Text;

namespace Api.AppCore.API_Common.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Tools
    {
        #region --> Public static methods. <--

        public static bool IsPropertyExist(dynamic obj, string name)
        {
            if (obj is ExpandoObject) return ((IDictionary<string, object>)obj).ContainsKey(name);
            else return obj.GetType().GetProperty(name) != null;
        }

        public static string GetTime(TimeSpan timeSpan) => string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

        public static string GetSize(dynamic obj)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = default;
            int order = 0;

            try
            {
                len = JsonConvert.SerializeObject(obj).Length;

                while (len >= 1024 &&
                       order < sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }
            }
            catch { }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        public static DateTime GetDateTimeNow() => DateTime.UtcNow.AddHours(-3);

        public static string RemoveAccents(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                StringBuilder sbReturn = new();
                var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

                foreach (char letter in arrayText)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                        sbReturn.Append(letter);
                }

                return sbReturn.ToString();
            }
            else return text;
        }

        public static string GlobalFinally(dynamic serviceResponse, Stopwatch stopwatch)
        {
            try
            {
                stopwatch.Stop();
                return " | Em " + GetDateTimeNow().ToString("dd/MM/yyyy hh:mm:ss") + " trafegou " + GetSize(serviceResponse.obj) + " em " + GetTime(stopwatch.Elapsed) + ".";
            }
            catch { return " | Em dd/MM/yyyy hh:mm:ss trafegou 0B em 00h:00m:00s:001ms."; }
            finally { GC.Collect(); }
        }

        #endregion --> Public static methods. <--
    }
}
