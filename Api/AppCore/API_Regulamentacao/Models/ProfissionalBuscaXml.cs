using System.Xml.Serialization;

namespace Api.AppCore.API_Regulamentacao.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    [XmlRoot(ElementName = "rss")]
    public class ProfissionalBuscaXml
    {
        [XmlElement("channel")]
        public ProfissionalBusca profissionalBusca { get; set; }
    }
}
