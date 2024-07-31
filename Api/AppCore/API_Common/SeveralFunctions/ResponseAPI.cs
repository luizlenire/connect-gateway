using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Api.AppCore.API_Common.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ResponseApi : ControllerBase
    {
        #region --> Public methods. <--

        public dynamic ProcessException(dynamic serviceResponse, Exception ex)
        {
            if (Tools.IsPropertyExist(serviceResponse, "obj")) serviceResponse.obj = default(dynamic);
            else if (Tools.IsPropertyExist(serviceResponse, "Object")) serviceResponse.Object = default(dynamic);

            if (Tools.IsPropertyExist(serviceResponse, "message"))
            {
                serviceResponse.message = "Ocorreu uma falha grave ao executar esta ação.";
                serviceResponse.message += "M¹: " + ex.Message + " | S²: " + ex.StackTrace;

                if (ex.InnerException != default)
                {
                    StringBuilder stringBuilder = new();
                    GetInnerException(ex, stringBuilder, 0);

                    serviceResponse.message += " | I³: " + stringBuilder;
                }
            }
            else if (Tools.IsPropertyExist(serviceResponse, "mensagem"))
            {
                serviceResponse.mensagem = "Ocorreu uma falha grave ao executar esta ação.";
                serviceResponse.mensagem += "M¹: " + ex.Message + " | S²: " + ex.StackTrace;

                if (ex.InnerException != default)
                {
                    StringBuilder stringBuilder = new();
                    GetInnerException(ex, stringBuilder, 0);

                    serviceResponse.mensagem += " | I³: " + stringBuilder;
                }
            }

            return serviceResponse;
        }

        #endregion --> Public methods. <--

        #region --> Private methods. <--

        private void GetInnerException(Exception exception, StringBuilder stringBuilder, int level)
        {
            string indent = new(' ', level);

            if (level > 0) stringBuilder.AppendLine(indent + "=== INNER EXCEPTION ===");

            append("Message");
            append("HResult");
            append("HelpLink");
            append("Source");
            append("StackTrace");
            append("TargetSite");

            foreach (DictionaryEntry item in exception.Data) stringBuilder.AppendFormat("{0} {1} = {2}{3}", indent, item.Key, item.Value, Environment.NewLine);

            if (exception.InnerException != default) GetInnerException(exception.InnerException, stringBuilder, ++level);

            #region --> Sub-methods. <--

            void append(string prop)
            {
                PropertyInfo propertyInfo = exception.GetType().GetProperty(prop);
                object obj = propertyInfo.GetValue(exception);

                if (obj != default) stringBuilder.AppendFormat("{0}{1}: {2}{3}", indent, prop, obj.ToString(), Environment.NewLine);
            }

            #endregion --> Sub-methods. <--
        }

        #endregion --> Private methods. <--
    }
}
