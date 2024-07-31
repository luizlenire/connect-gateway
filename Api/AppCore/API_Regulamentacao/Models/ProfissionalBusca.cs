using System.Xml.Serialization;

namespace Api.AppCore.API_Regulamentacao.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ProfissionalBusca
    {
        #region --> Public properties. <--      

        public string url { get; set; }

        public long total { get; set; }

        public bool status { get; set; }

        public string mensagem { get; set; }

        public long AppCore_limite { get; set; }

        public string AppCore_consultas { get; set; }

        [XmlElement("item")]
        public List<ProfissionalBuscaDetalhe> item { get; set; }

        #endregion --> Public properties. <--
    }
}
