using Microsoft.AspNetCore.Mvc;
using RestCountriesAPI_EdgarsSvarups.Interfaces;
using RestCountriesAPI_EdgarsSvarups.Methods;
using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Controllers
{
    public class CountriesController : ControllerBase
    {
        public static ICountry _countries;
        
        public CountriesController(ICountry countries)
        {
            _countries = countries;
        }

        public class AllEuCountries
        {
            public static Task<List<CountryModel>> ReturnAllEuropeanCountries = _countries.GetCountries();
        }



        [HttpGet("countries/{name}")]
        public async Task<IActionResult> ReturnCountryWithMatchingName(string name)
        {
            var europeanCountryWithoutName = await CountryService.ReturnCountryWithoutName(name);

            if (europeanCountryWithoutName == null) throw new ArgumentNullException(nameof(europeanCountryWithoutName));

            if (CountryService.IsEuropeanCountry(await AllEuCountries.ReturnAllEuropeanCountries, europeanCountryWithoutName))
                return Ok(europeanCountryWithoutName);

            return BadRequest();
        }

        [HttpGet("/Countries/TopTenByPopulation")]
        public async Task<IActionResult> ReturnTopTenEuropeanCountriesByPopulation()
        {
            return Ok(CountryService.SortEuropeanCountriesByPopulation(await AllEuCountries.ReturnAllEuropeanCountries).Take(10));
        }

        [HttpGet("/Countries/TopTenByDensity")]
        public async Task<IActionResult> ReturnTopTenEuropeanCountriesByDensity()
        {
            return Ok(CountryService.SortEuropeanCountriesByDensity(await AllEuCountries.ReturnAllEuropeanCountries).Take(10));
        }
    }
}
