using Refit;
using RestCountriesAPI_EdgarsSvarups.Interfaces;
using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Methods;

public class CountryService : ICountryService
{
    private readonly ICountry _countryService;

    public CountryService(ICountry country)
    {
        _countryService = country;
    }

    public async Task<List<CountryModel>> AllEuropeanCountries() 
    {
        return await _countryService.GetCountries();
    }

    public async Task<IEnumerable<CountryModel>> ReturnEuropeUnionCountries()
    {
        var countries = await AllEuropeanCountries();

        return countries.Where(countryModel => countryModel.Independent).ToList();
    }

    public bool IsEuropeanCountry(List<CountryModel> europeUnionCountries, SingleCountryModel country)
    {
        return europeUnionCountries.Any(countryModel => countryModel.NativeName == country.NativeName);
    }

    public async Task<SingleCountryModel?> ReturnCountryWithoutName(string name)
    {
        var listOfCountries = await _countryService.GetCountryByName(name);

        var countryWithoutName = listOfCountries.Select(countryModel =>
            new SingleCountryModel
            {
                Area = countryModel.Area,
                Population = countryModel.Population,
                TopLevelDomain = countryModel.TopLevelDomain!,
                NativeName = countryModel.NativeName!
            }).FirstOrDefault();

        return countryWithoutName;
    }

    public async Task<IEnumerable<CountryModel>> SortEuropeanCountriesByPopulation()
    {
        var europeanCountries = await ReturnEuropeUnionCountries();

        return europeanCountries.OrderByDescending(countryModel => countryModel.Population).ToList();
    }

    public async Task<IEnumerable<CountryModel>> SortEuropeanCountriesByDensity()
    {
        var europeanCountries = await ReturnEuropeUnionCountries();

        return europeanCountries.OrderByDescending(countryModel => countryModel.Population / countryModel.Area)
            .ToList();
    }
}