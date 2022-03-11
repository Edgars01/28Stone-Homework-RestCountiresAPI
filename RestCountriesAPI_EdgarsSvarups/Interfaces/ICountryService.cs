using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Interfaces;

public interface ICountryService
{
    Task<List<CountryModel>> AllEuropeanCountries();

    Task<IEnumerable<CountryModel>> ReturnEuropeUnionCountries();

    bool IsEuropeanCountry(List<CountryModel> europeUnionCountries, SingleCountryModel country);

    Task<SingleCountryModel?> ReturnCountryWithoutName(string name);

    Task<IEnumerable<CountryModel>> SortEuropeanCountriesByPopulation();

    Task<IEnumerable<CountryModel>> SortEuropeanCountriesByDensity();
}