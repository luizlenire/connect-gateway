namespace Api.AppCore.API_BrasilAPI.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class LegalPersonBrasilApi
    {
        #region --> Public properties. <--

        public string uf { get; set; }

        public string cep { get; set; }

        public List<LegalPersonQsaBrasilApi> qsa { get; set; }

        public string cnpj { get; set; }

        public string pais { get; set; }

        public string email { get; set; }

        public string porte { get; set; }

        public string bairro { get; set; }

        public string numero { get; set; }

        public string ddd_fax { get; set; }

        public string municipio { get; set; }

        public string logradouro { get; set; }

        public int cnae_fiscal { get; set; }

        public string codigo_pais { get; set; }

        public string complemento { get; set; }

        public int codigo_porte { get; set; }

        public string razao_social { get; set; }

        public string nome_fantasia { get; set; }

        public int capital_social { get; set; }

        public string ddd_telefone_1 { get; set; }

        public string ddd_telefone_2 { get; set; }

        public bool opcao_pelo_mei { get; set; }

        public string descricao_porte { get; set; }

        public int codigo_municipio { get; set; }

        public List<LegalPersonCnaesSecundarioBrasilApi> cnaes_secundarios { get; set; }

        public string natureza_juridica { get; set; }

        public string situacao_especial { get; set; }

        public bool opcao_pelo_simples { get; set; }

        public int situacao_cadastral { get; set; }

        public string data_opcao_pelo_mei { get; set; }

        public string data_exclusao_do_mei { get; set; }

        public string cnae_fiscal_descricao { get; set; }

        public int codigo_municipio_ibge { get; set; }

        public string data_inicio_atividade { get; set; }

        public string data_situacao_especial { get; set; }

        public string data_opcao_pelo_simples { get; set; }

        public string data_situacao_cadastral { get; set; }

        public string nome_cidade_no_exterior { get; set; }

        public int codigo_natureza_juridica { get; set; }

        public string data_exclusao_do_simples { get; set; }

        public int motivo_situacao_cadastral { get; set; }

        public string ente_federativo_responsavel { get; set; }

        public int identificador_matriz_filial { get; set; }

        public int qualificacao_do_responsavel { get; set; }

        public string descricao_situacao_cadastral { get; set; }

        public string descricao_tipo_de_logradouro { get; set; }

        public string descricao_motivo_situacao_cadastral { get; set; }

        public string descricao_identificador_matriz_filial { get; set; }

        #endregion --> Public properties. <--

        #region --> Public methods. <--

        public void Clean()
        {
            uf = uf == default ? default : uf.ToUpper().Trim();
            cep = cep == default ? default : cep.Replace(".", default).Replace("-", default).ToUpper().Trim();

            if (qsa != default &&
                qsa.Count != default)
            {
                qsa.ForEach(x => x.pais = x.pais == default ? default : x.pais.ToUpper().Trim());
                qsa.ForEach(x => x.nome_socio = x.nome_socio == default ? default : x.nome_socio.ToUpper().Trim());
                qsa.ForEach(x => x.codigo_pais = x.codigo_pais == default ? default : x.codigo_pais.ToUpper().Trim());
                qsa.ForEach(x => x.faixa_etaria = x.faixa_etaria == default ? default : x.faixa_etaria.ToUpper().Trim());
                qsa.ForEach(x => x.cnpj_cpf_do_socio = x.cnpj_cpf_do_socio == default ? default : x.cnpj_cpf_do_socio.ToUpper().Trim());
                qsa.ForEach(x => x.qualificacao_socio = x.qualificacao_socio == default ? default : x.qualificacao_socio.ToUpper().Trim());
                qsa.ForEach(x => x.codigo_faixa_etaria = x.codigo_faixa_etaria == default ? default : x.codigo_faixa_etaria);
                qsa.ForEach(x => x.data_entrada_sociedade = x.data_entrada_sociedade == default ? default : x.data_entrada_sociedade.ToUpper().Trim());
                qsa.ForEach(x => x.identificador_de_socio = x.identificador_de_socio == default ? default : x.identificador_de_socio);
                qsa.ForEach(x => x.cpf_representante_legal = x.cpf_representante_legal == default ? default : x.cpf_representante_legal.ToUpper().Trim());
                qsa.ForEach(x => x.nome_representante_legal = x.nome_representante_legal == default ? default : x.nome_representante_legal.ToUpper().Trim());
                qsa.ForEach(x => x.codigo_qualificacao_socio = x.codigo_qualificacao_socio == default ? default : x.codigo_qualificacao_socio);
                qsa.ForEach(x => x.qualificacao_representante_legal = x.qualificacao_representante_legal == default ? default : x.qualificacao_representante_legal.ToUpper().Trim());
                qsa.ForEach(x => x.codigo_qualificacao_representante_legal = x.codigo_qualificacao_representante_legal == default ? default : x.codigo_qualificacao_representante_legal);
            }

            cnpj = cnpj == default ? default : cnpj.Replace(".", default).Replace("/", default).Replace("-", default).ToUpper().Trim();
            pais = pais == default ? default : pais.ToUpper().Trim();
            porte = porte == default ? default : porte.ToUpper().Trim();
            bairro = bairro == default ? default : bairro.ToUpper().Trim();
            numero = numero == default ? default : numero.ToUpper().Trim();
            ddd_fax = ddd_fax == default ? default : ddd_fax.ToUpper().Trim();
            municipio = municipio == default ? default : municipio.ToUpper().Trim();
            logradouro = logradouro == default ? default : logradouro.ToUpper().Trim();
            cnae_fiscal = cnae_fiscal == default ? default : cnae_fiscal;
            codigo_pais = codigo_pais == default ? default : codigo_pais.ToUpper().Trim();
            complemento = complemento == default ? default : complemento.ToUpper().Trim();
            codigo_porte = codigo_porte == default ? default : codigo_porte;
            razao_social = razao_social == default ? default : razao_social.ToUpper().Trim();
            nome_fantasia = nome_fantasia == default ? default : nome_fantasia.ToUpper().Trim();
            capital_social = capital_social == default ? default : capital_social;
            ddd_telefone_1 = ddd_telefone_1 == default ? default : ddd_telefone_1.ToUpper().Trim();
            ddd_telefone_2 = ddd_telefone_2 == default ? default : ddd_telefone_2.ToUpper().Trim();
            opcao_pelo_mei = opcao_pelo_mei == default ? default : opcao_pelo_mei;
            descricao_porte = descricao_porte == default ? default : descricao_porte.ToUpper().Trim();

            if (cnaes_secundarios != default &&
                cnaes_secundarios.Count != default)
            {
                cnaes_secundarios.ForEach(x => x.codigo = x.codigo == default ? default : x.codigo);
                cnaes_secundarios.ForEach(x => x.codigo = x.codigo == default ? default : x.codigo);
            }

            codigo_municipio = codigo_municipio == default ? default : codigo_municipio;
            natureza_juridica = natureza_juridica == default ? default : natureza_juridica.ToUpper().Trim();
            situacao_especial = situacao_especial == default ? default : situacao_especial.ToUpper().Trim();
            opcao_pelo_simples = opcao_pelo_simples == default ? default : opcao_pelo_simples;
            situacao_cadastral = situacao_cadastral == default ? default : situacao_cadastral;
            data_opcao_pelo_mei = data_opcao_pelo_mei == default ? default : data_opcao_pelo_mei.ToUpper().Trim();
            data_exclusao_do_mei = data_exclusao_do_mei == default ? default : data_exclusao_do_mei.ToUpper().Trim();
            cnae_fiscal_descricao = cnae_fiscal_descricao == default ? default : cnae_fiscal_descricao.ToUpper().Trim();
            data_inicio_atividade = data_inicio_atividade == default ? default : data_inicio_atividade.ToUpper().Trim();
            data_situacao_especial = data_situacao_especial == default ? default : data_situacao_especial.ToUpper().Trim();
            data_opcao_pelo_simples = data_opcao_pelo_simples == default ? default : data_opcao_pelo_simples.ToUpper().Trim();
            data_situacao_cadastral = data_situacao_cadastral == default ? default : data_situacao_cadastral.ToUpper().Trim();
            nome_cidade_no_exterior = nome_cidade_no_exterior == default ? default : nome_cidade_no_exterior.ToUpper().Trim();
            codigo_natureza_juridica = codigo_natureza_juridica == default ? default : codigo_natureza_juridica;
            data_exclusao_do_simples = data_exclusao_do_simples == default ? default : data_exclusao_do_simples.ToUpper().Trim();

            motivo_situacao_cadastral = motivo_situacao_cadastral == default ? default : motivo_situacao_cadastral;
            ente_federativo_responsavel = ente_federativo_responsavel == default ? default : ente_federativo_responsavel.ToUpper().Trim();
            identificador_matriz_filial = identificador_matriz_filial == default ? default : identificador_matriz_filial;
            qualificacao_do_responsavel = qualificacao_do_responsavel == default ? default : qualificacao_do_responsavel;
            descricao_situacao_cadastral = descricao_situacao_cadastral == default ? default : descricao_situacao_cadastral.ToUpper().Trim();
            descricao_tipo_de_logradouro = descricao_tipo_de_logradouro == default ? default : descricao_tipo_de_logradouro.ToUpper().Trim();
            descricao_motivo_situacao_cadastral = descricao_motivo_situacao_cadastral == default ? default : descricao_motivo_situacao_cadastral.ToUpper().Trim();

            if (string.IsNullOrEmpty(nome_fantasia)) nome_fantasia = razao_social;
            if (string.IsNullOrEmpty(razao_social)) razao_social = nome_fantasia;
        }

        #endregion --> Public methods. <--
    }
}
