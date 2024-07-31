namespace Api.AppCore.API_Regulamentacao.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ProfissionalBuscaDetalhe
    {
        #region --> Public properties. <--

        public string tipo { get; set; }

        public string nome { get; set; }

        public string numero { get; set; }

        public string profissao { get; set; }

        public string uf { get; set; }

        public string situacao { get; set; }

        #endregion --> Public properties. <--
    }
}
