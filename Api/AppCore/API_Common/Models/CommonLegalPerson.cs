using Api.AppCore.API_BrasilAPI.Models;
using Api.AppCore.API_ReceitaWsApi.Models;
using Newtonsoft.Json;

namespace Api.AppCore.API_Common.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class CommonLegalPerson
    {
        #region --> Public properties. <--

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string DataAbertura { get; set; }

        public string Situacao { get; set; }

        public string DataSituacao { get; set; }

        public string NomeSocio { get; set; }

        public string CargoSocio { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string PontoReferencia { get; set; }

        public string Bairro { get; set; }

        public string ObjOrigin { get; set; }

        #endregion --> Public properties. <--

        #region --> Constructors. <--

        public CommonLegalPerson() { }

        public CommonLegalPerson(LegalPersonReceitaWsApi legalPersonReceitaWsApi)
        {
            ObjOrigin = JsonConvert.SerializeObject(legalPersonReceitaWsApi);

            legalPersonReceitaWsApi.Clean();

            Cnpj = legalPersonReceitaWsApi.cnpj;
            RazaoSocial = legalPersonReceitaWsApi.nome;
            NomeFantasia = legalPersonReceitaWsApi.fantasia;
            DataAbertura = legalPersonReceitaWsApi.abertura;
            Situacao = legalPersonReceitaWsApi.situacao;
            DataSituacao = legalPersonReceitaWsApi.data_situacao;

            if (legalPersonReceitaWsApi.qsa.Count != default)
            {
                NomeSocio = legalPersonReceitaWsApi.qsa[0].nome;
                CargoSocio = legalPersonReceitaWsApi.qsa[0].qual;
            }

            Telefone = legalPersonReceitaWsApi.telefone;
            Email = legalPersonReceitaWsApi.email;
            Cep = legalPersonReceitaWsApi.cep;
            Logradouro = legalPersonReceitaWsApi.logradouro;
            Numero = legalPersonReceitaWsApi.numero;
            Complemento = legalPersonReceitaWsApi.complemento;
            Bairro = legalPersonReceitaWsApi.bairro;
        }

        public CommonLegalPerson(LegalPersonBrasilApi legalPersonBrasilApi)
        {
            ObjOrigin = JsonConvert.SerializeObject(legalPersonBrasilApi);

            legalPersonBrasilApi.Clean();

            Cnpj = legalPersonBrasilApi.cnpj;
            RazaoSocial = legalPersonBrasilApi.razao_social;
            NomeFantasia = legalPersonBrasilApi.nome_fantasia;
            DataAbertura = legalPersonBrasilApi.data_inicio_atividade;
            Situacao = legalPersonBrasilApi.descricao_situacao_cadastral;
            DataSituacao = legalPersonBrasilApi.data_situacao_cadastral;

            if (legalPersonBrasilApi.qsa.Count != default)
            {
                NomeSocio = legalPersonBrasilApi.qsa[0].nome_socio;
                CargoSocio = legalPersonBrasilApi.qsa[0].qualificacao_socio;
            }

            Telefone = legalPersonBrasilApi.ddd_telefone_1;
            //Email = pessoaJuridicaBrasilApi.email;
            Cep = legalPersonBrasilApi.cep;
            Logradouro = legalPersonBrasilApi.logradouro;
            Numero = legalPersonBrasilApi.numero;
            Complemento = legalPersonBrasilApi.complemento;
            Bairro = legalPersonBrasilApi.bairro;
        }

        #endregion --> Constructors. <--
    }
}
