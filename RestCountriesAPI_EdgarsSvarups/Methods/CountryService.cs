using RestCountriesAPI_EdgarsSvarups.Interfaces;
using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Methods;

public class CountryService : ICountryService
{
    public readonly ICountry _countryService;

    public CountryService(ICountry countryService)
    {
        _countryService = countryService;
    }

    public async Task<List<CountryModel>> AllCountries() 
    {
        return await _countryService.GetCountries();
    }

    public async Task<IEnumerable<CountryModel>> ReturnEuropeUnionCountries()
    {
        var countries = await AllCountries();

        return countries.Where(countryModel => countryModel.Independent).ToList();
    }
    public async Task<bool> IsEuropeanCountry(string name)
    {
        var europeUnionCountries = await ReturnEuropeUnionCountries();

        return europeUnionCountries.Any(c => c.Name.ToString().ToLower() == name.ToLower());
    }
    
    public async Task<SingleCountryModel?> ReturnCountryWithoutName(string name)
    {
        var listOfCountries = await _countryService.GetCountryByName(name);
        
        var countryWithoutName = listOfCountries
            .Select(countryModel => new SingleCountryModel
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