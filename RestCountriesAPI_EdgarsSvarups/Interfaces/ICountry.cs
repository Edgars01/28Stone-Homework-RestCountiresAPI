using Refit;
using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Interfaces
{
    public interface ICountry
    {
        [Get("/v2/regionalbloc/eu")]
        Task<List<CountryModel>> GetCountries();

        [Get("/v2/name/{name}")]
        Task<List<CountryModel>> GetCountryByName(string name);
    }
}
