using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Interfaces;

public interface ICountryService
{
    Task<List<CountryModel>> AllCountries();

    Task<IEnumerable<CountryModel>> ReturnEuropeUnionCountries();

    Task<bool> IsEuropeanCountry(string country);

    Task<SingleCountryModel?> ReturnCountryWithoutName(string name);

    Task<IEnumerable<CountryModel>> SortEuropeanCountriesByPopulation();

    Task<IEnumerable<CountryModel>> SortEuropeanCountriesByDensity();
}