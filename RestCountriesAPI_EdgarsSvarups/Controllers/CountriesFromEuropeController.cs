using Microsoft.AspNetCore.Mvc;
using RestCountriesAPI_EdgarsSvarups.Interfaces;
using RestCountriesAPI_EdgarsSvarups.Methods;

namespace RestCountriesAPI_EdgarsSvarups.Controllers;

public class CountriesController : ControllerBase
{
    public ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("countries/{name}")]
    public async Task<IActionResult> ReturnCountryWithMatchingName(string name)
    {
        var europeanCountryWithoutName = await _countryService.ReturnCountryWithoutName(name);

        if (europeanCountryWithoutName == null) throw new ArgumentNullException(nameof(europeanCountryWithoutName));

        if (_countryService.IsEuropeanCountry(await _countryService.AllEuropeanCountries(), europeanCountryWithoutName))
            return Ok(europeanCountryWithoutName);

        return BadRequest();
    }

    [HttpGet("/Countries/TopTenByPopulation")]
    public async Task<IActionResult> ReturnTopTenEuropeanCountriesByPopulation()
    {
        return Ok((await _countryService.SortEuropeanCountriesByPopulation()).Take(10));
    }

    [HttpGet("/Countries/TopTenByDensity")]
    public async Task<IActionResult> ReturnTopTenEuropeanCountriesByDensity()
    {
        return Ok((await _countryService.SortEuropeanCountriesByDensity()).Take(10));
    }
}