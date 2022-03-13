using Microsoft.AspNetCore.Mvc;
using RestCountriesAPI_EdgarsSvarups.Interfaces;

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
        if (await _countryService.IsEuropeanCountry(name) == false) return BadRequest("Only European countries");

        return Ok(await _countryService.ReturnCountryWithoutName(name));
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