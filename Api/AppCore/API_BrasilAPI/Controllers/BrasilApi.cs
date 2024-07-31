using Api.AppCore.API_BrasilAPI.Models;
using Api.AppCore.API_Common.Controllers;
using Api.AppCore.API_Common.Models;
using Api.AppCore.API_Common.SeveralFunctions;

namespace Api.AppCore.API_BrasilAPI.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class BrasilApi : RequestResponse
    {
        #region --> Public methods. <--

        public ServiceResponse<LegalPersonBrasilApi> GetLegalPerson(string cnpj)
        {
            ServiceResponse<LegalPersonBrasilApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "cnpj/v1/", cnpj, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<ZipCodeBrasilApi> GetZipCodeV1(string cep)
        {
            ServiceResponse<ZipCodeBrasilApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "cep/v1/", cep, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<ZipCodeBrasilApi> GetZipCodeV2(string cep)
        {
            ServiceResponse<ZipCodeBrasilApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "cep/v2/", cep, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<List<HolidaysBrasilApi>> GetHolidays(DateTime dateTime)
        {
            ServiceResponse<List<HolidaysBrasilApi>> serviceResponse = new() { obj = [] };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "feriados/v1/", dateTime.Year.ToString(), serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<List<IbgeCodeCityBrasilApi>> GetIbgeCodeCity(string uf)
        {
            ServiceResponse<List<IbgeCodeCityBrasilApi>> serviceResponse = new() { obj = [] };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "ibge/municipios/v1/", uf, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<List<AdditionalInfoUfBrasilApi>> GetAdditionalInfoUf(string uf)
        {
            ServiceResponse<List<AdditionalInfoUfBrasilApi>> serviceResponse = new() { obj = [] };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "ibge/uf/v1/", uf, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<BankBrasilApi> GetBank(string code)
        {
            ServiceResponse<BankBrasilApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "banks/v1/", code, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<List<BankBrasilApi>> GetBanks()
        {
            ServiceResponse<List<BankBrasilApi>> serviceResponse = new() { obj = [] };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "banks/v1/", default, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        public ServiceResponse<DddBrasilApi> GetDdd(string code)
        {
            ServiceResponse<DddBrasilApi> serviceResponse = new() { obj = new() };

            serviceResponse.obj = Get(GlobalAtributtes.UrlBrasilApi + "ddd/v1/", code, serviceResponse.obj.GetType());
            serviceResponse = Process(serviceResponse);

            return serviceResponse;
        }

        #endregion --> Public methods. <--      
    }
}
