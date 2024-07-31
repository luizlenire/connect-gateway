using Api.AppCore.API_AwesomeApi.Models;
using Api.AppCore.API_BrasilAPI.Models;
using Api.AppCore.API_Common.SeveralFunctions;
using Newtonsoft.Json;

namespace Api.AppCore.API_Common.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class CommonZipCode
    {
        #region --> Public properties. <--

        public string Cep { get; set; }

        public string TipoLogradouro { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string ObjOrigin { get; set; }

        #endregion --> Public properties. <--

        #region --> Constructors. <--

        public CommonZipCode() { }

        public CommonZipCode(AddressAwesomeApi addressAwesomeApi)
        {
            ObjOrigin = JsonConvert.SerializeObject(addressAwesomeApi);

            Cep = addressAwesomeApi.cep;
            TipoLogradouro = addressAwesomeApi.address_type != default ? Tools.RemoveAccents(addressAwesomeApi.address_type.ToUpper()) : default;
            Logradouro = addressAwesomeApi.address_name != default ? Tools.RemoveAccents(addressAwesomeApi.address_name.ToUpper()) : default;
            Complemento = default;
            Bairro = addressAwesomeApi.district != default ? Tools.RemoveAccents(addressAwesomeApi.district.ToUpper()) : default;
            Cidade = addressAwesomeApi.city != default ? Tools.RemoveAccents(addressAwesomeApi.city.ToUpper()) : default;
            Estado = addressAwesomeApi.state != default ? Tools.RemoveAccents(addressAwesomeApi.state.ToUpper()) : default;
            Pais = "BRASIL";
            Latitude = addressAwesomeApi.lat;
            Longitude = addressAwesomeApi.lng;
        }
        public CommonZipCode(ZipCodeBrasilApi zipCodeBrasilApi)
        {
            ObjOrigin = JsonConvert.SerializeObject(zipCodeBrasilApi);

            Cep = zipCodeBrasilApi.cep;
            TipoLogradouro = zipCodeBrasilApi.street.Split(" ")[0] != default ? Tools.RemoveAccents(zipCodeBrasilApi.street.Split(" ")[0].ToUpper()) : default;
            Logradouro = zipCodeBrasilApi.street != default ? Tools.RemoveAccents(zipCodeBrasilApi.street.Replace(zipCodeBrasilApi.street.Split(" ")[0], default).Trim().ToUpper()) : default;
            Complemento = default;
            Bairro = zipCodeBrasilApi.neighborhood != default ? Tools.RemoveAccents(zipCodeBrasilApi.neighborhood.ToUpper()) : default;
            Cidade = zipCodeBrasilApi.city != default ? Tools.RemoveAccents(zipCodeBrasilApi.city.ToUpper()) : default;
            Estado = zipCodeBrasilApi.state != default ? Tools.RemoveAccents(zipCodeBrasilApi.state.ToUpper()) : default;
            Pais = "BRASIL";

            if (zipCodeBrasilApi.location != default &&
                zipCodeBrasilApi.location.coordinates != default)
            {
                Latitude = zipCodeBrasilApi.location.coordinates.latitude;
                Longitude = zipCodeBrasilApi.location.coordinates.longitude;
            }
        }

        #endregion --> Construtors. <--     
    }
}
