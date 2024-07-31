namespace Api.AppCore.API_BrasilAPI.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class LegalPersonQsaBrasilApi
    {
        #region --> Public properties. <--

        public string pais { get; set; }

        public string nome_socio { get; set; }

        public string codigo_pais { get; set; }

        public string faixa_etaria { get; set; }

        public string cnpj_cpf_do_socio { get; set; }

        public string qualificacao_socio { get; set; }

        public int codigo_faixa_etaria { get; set; }

        public string data_entrada_sociedade { get; set; }

        public int identificador_de_socio { get; set; }

        public string cpf_representante_legal { get; set; }

        public string nome_representante_legal { get; set; }

        public int codigo_qualificacao_socio { get; set; }

        public string qualificacao_representante_legal { get; set; }

        public int codigo_qualificacao_representante_legal { get; set; }

        #endregion --> Public properties. <--
    }
}
