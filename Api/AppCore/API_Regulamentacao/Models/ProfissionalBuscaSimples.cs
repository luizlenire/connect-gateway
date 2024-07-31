namespace Api.AppCore.API_Regulamentacao.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public class ProfissionalBuscaSimples
    {
        #region --> Public propeties. <--

        public string NomeProfissional { get; set; }

        public string NumeroConselho { get; set; }

        public string Situacao { get; set; }

        #endregion --> Public propeties. <--

        #region --> Constructors. <--

        public ProfissionalBuscaSimples(ProfissionalBuscaDetalhe profissionalBuscaDetalhe)
        {
            NomeProfissional = profissionalBuscaDetalhe.nome;
            NumeroConselho = profissionalBuscaDetalhe.numero;
            Situacao = profissionalBuscaDetalhe.situacao;
        }

        #endregion --> Constructors. <--
    }
}
