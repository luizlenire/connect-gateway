namespace Api.AppCore.API_ReceitaWsApi.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class LegalPersonReceitaWsApi
    {
        #region --> Public properties. <--

        public string abertura { get; set; }

        public string situacao { get; set; }

        public string tipo { get; set; }

        public string nome { get; set; }

        public string fantasia { get; set; }

        public string porte { get; set; }

        public string natureza_juridica { get; set; }

        public List<LegalPersonReceitaWsApiQsa> qsa { get; set; }

        public string logradouro { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        public string municipio { get; set; }

        public string bairro { get; set; }

        public string uf { get; set; }

        public string cep { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public string data_situacao { get; set; }

        public string motivo_situacao { get; set; }

        public string cnpj { get; set; }

        public DateTime ultima_atualizacao { get; set; }

        public string status { get; set; }

        public string efr { get; set; }

        public string situacao_especial { get; set; }

        public string data_situacao_especial { get; set; }

        public List<LegalPersonReceitaWsApiAtividadePrincipal> atividade_principal { get; set; }

        public List<LegalPersonReceitaWsApiAtividadesSecundaria> atividades_secundarias { get; set; }

        public string capital_social { get; set; }

        public LegalPersonReceitaWsApiExtra extra { get; set; }

        public LegalPersonReceitaWsApiBilling billing { get; set; }

        #endregion --> Public properties. <--

        #region --> Public methods. <--

        public void Clean()
        {
            status = status == default ? default : status.ToUpper().Trim();
            cnpj = cnpj == default ? default : cnpj.Replace(".", default).Replace("/", default).Replace("-", default).ToUpper().Trim();
            tipo = tipo == default ? default : tipo.ToUpper().Trim();
            abertura = abertura == default ? default : abertura.ToUpper().Trim();
            nome = nome == default ? default : nome.ToUpper().Trim();
            fantasia = fantasia == default ? default : fantasia.ToUpper().Trim();

            if (string.IsNullOrEmpty(fantasia)) fantasia = nome;
            if (string.IsNullOrEmpty(nome)) nome = fantasia;

            if (atividade_principal != default &&
                atividade_principal.Count != default)
            {
                atividade_principal.ForEach(x => x.code = x.code == default ? default : x.code.ToUpper().Trim());
                atividade_principal.ForEach(x => x.text = x.text == default ? default : x.text.ToUpper().Trim());
            }

            if (atividades_secundarias != default &&
                atividades_secundarias.Count != default)
            {
                atividades_secundarias.ForEach(x => x.code = x.code == default ? default : x.code.ToUpper().Trim());
                atividades_secundarias.ForEach(x => x.text = x.text == default ? default : x.text.ToUpper().Trim());
            }

            natureza_juridica = natureza_juridica == default ? default : natureza_juridica.ToUpper().Trim();
            logradouro = logradouro == default ? default : logradouro.ToUpper().Trim();
            numero = numero == default ? default : numero.ToUpper().Trim();
            complemento = complemento == default ? default : complemento.ToUpper().Trim();
            cep = cep == default ? default : cep.Replace(".", default).Replace("-", default).ToUpper().Trim();
            bairro = bairro == default ? default : bairro.ToUpper().Trim();
            municipio = municipio == default ? default : municipio.ToUpper().Trim();
            uf = uf == default ? default : uf.ToUpper().Trim();
            email = email == default ? default : email.ToUpper().Trim();
            telefone = telefone == default ? default : telefone.ToUpper().Trim();
            efr = efr == default ? default : efr.ToUpper().Trim();
            situacao = situacao == default ? default : situacao.ToUpper().Trim();
            data_situacao = data_situacao == default ? default : data_situacao.ToUpper().Trim();
            motivo_situacao = motivo_situacao == default ? default : motivo_situacao.ToUpper().Trim();
            situacao_especial = situacao_especial == default ? default : situacao_especial.ToUpper().Trim();
            data_situacao_especial = data_situacao_especial == default ? default : data_situacao_especial.ToUpper().Trim();
            capital_social = capital_social == default ? default : capital_social.ToUpper().Trim();

            if (qsa != default &&
                qsa.Count != default)
            {
                qsa.ForEach(x => x.nome = x.nome == default ? default : x.nome.ToUpper().Trim());
                qsa.ForEach(x => x.qual = x.qual == default ? default : x.qual.ToUpper().Trim());
                qsa.ForEach(x => x.pais_origem = x.pais_origem == default ? default : x.pais_origem.ToUpper().Trim());
                qsa.ForEach(x => x.nome_rep_legal = x.nome_rep_legal == default ? default : x.nome_rep_legal.ToUpper().Trim());
                qsa.ForEach(x => x.qual_rep_legal = x.qual_rep_legal == default ? default : x.qual_rep_legal.ToUpper().Trim());
            }
        }

        #endregion --> Public methods. <--
    }
}
