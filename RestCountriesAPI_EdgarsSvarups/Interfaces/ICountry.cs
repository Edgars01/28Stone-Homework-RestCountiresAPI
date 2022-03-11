using Refit;
using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Interfaces;

public interface ICountry
{
    [Get("/regionalbloc/eu")]
    Task<List<CountryModel>> GetCountries();

    [Get("/name/{name}")]
    Task<List<CountryModel>> GetCountryByName(string name);
}