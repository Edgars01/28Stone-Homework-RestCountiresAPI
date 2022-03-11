using RestCountriesAPI_EdgarsSvarups.Controllers;
using RestCountriesAPI_EdgarsSvarups.Models;

namespace RestCountriesAPI_EdgarsSvarups.Methods
{
    public class CountryService
    {
        // test
        // uztasitit ta lai - var euCountries = EuropeUnionCountry(country); - butu variable, jo ir divi duplicate leja kurus var replaceot ar 1

        public static Task<List<CountryModel>> AllEuropeanCountriesTaskList = CountriesController._countries.GetCountries();

        public List<CountryModel> AllEuropeanCountriesList = Task.FromResult(ReturnEuropeUnionCountry(AllEuropeanCountriesTaskList))


        // uztasitit ta lai - var euCountries = EuropeUnionCountry(country); - butu variable, jo ir divi duplicate leja kurus var replaceot ar 1

        // sis ir tas ko gribu parvest uz variable ---- var europeanCountries = ReturnEuropeUnionCountry(country);
        //test

        public static IEnumerable<CountryModel> ReturnEuropeUnionCountry(List<CountryModel> country)
        {
            return country.Where(countryModel => countryModel.Independent);
        }
        public static bool IsEuropeanCountry(List<CountryModel> europeUnionCountries, SingleCountryModel name)
        {
            return europeUnionCountries.Any(CountryModel => CountryModel.NativeName == name.NativeName);
        }

        public static async Task<SingleCountryModel?> ReturnCountryWithoutName(string name)
        {
            var ListOfCountries = await CountriesController._countries.GetCountryByName(name);

            var countryWithoutName = ListOfCountries.Select(countryModel =>
                new SingleCountryModel
                {
                    Area = countryModel.Area,
                    Population = countryModel.Population,
                    TopLevelDomain = countryModel.TopLevelDomain!,
                    NativeName = countryModel.NativeName!
                }).FirstOrDefault();

            return countryWithoutName;
        }

        public static IEnumerable<CountryModel> SortEuropeanCountriesByPopulation(List<CountryModel> country)
        {
            var europeanCountries = ReturnEuropeUnionCountry(country);
            var TopTenEuropeanCountriesSortedBypopulation = europeanCountries.OrderByDescending(countryModel => countryModel.Population);

            return TopTenEuropeanCountriesSortedBypopulation;
        }

        public static IEnumerable<CountryModel> SortEuropeanCountriesByDensity(List<CountryModel> country)
        {
            // sitais --
            var europeanCountries = ReturnEuropeUnionCountry(country);
            // --
            return europeanCountries.OrderByDescending(countryModel => countryModel.Population / countryModel.Area);
        }
    }
}
