namespace Api.AppCore.API_Regulamentacao.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class ProfissionalRetorno
    {
        #region --> Public properties. <--

        public List<ProfissionalBuscaSimples> listProfissionalBuscaSimples { get; set; }

        public string retorno { get; set; }

        #endregion --> Public properties. <--
    }
}
