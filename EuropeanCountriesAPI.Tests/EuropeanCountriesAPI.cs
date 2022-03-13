using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Newtonsoft.Json;
using RestCountriesAPI_EdgarsSvarups.Interfaces;
using RestCountriesAPI_EdgarsSvarups.Methods;
using RestCountriesAPI_EdgarsSvarups.Models;
using Xunit;

namespace EuropeanCountriesAPI.Tests;

public class CountryServicesTests
{
    private readonly CountryService _countryService;
    private readonly Mock<ICountry> _countryMock = new();

    public CountryServicesTests()
    {
        _countryService = new CountryService(_countryMock.Object);
    }

    [Fact]
    public async Task ReturnCountryWithoutName_ShouldReturnCountryWithoutName_WhenCountryExists()
    {
        // Arrange
        var countryName = "Estonia";

        var countryObject = new List<CountryModel>
        {
            new()
            {
                Name = "Estonia",
                Area = 45227,
                Population = 1331057,
                TopLevelDomain = new List<string> {".ee"},
                NativeName = "Eesti"
            }
        };

        _countryMock.Setup(x => x.GetCountryByName(countryName))
            .ReturnsAsync(countryObject);

        var expected = new SingleCountryModel
        {
            Area = 45227,
            Population = 1331057,
            TopLevelDomain = new List<string> {".ee"},
            NativeName = "Eesti"
        };

        // Act
        var result = await _countryService.ReturnCountryWithoutName("Estonia");
        var expectedString = JsonConvert.SerializeObject(expected);
        var actualString = JsonConvert.SerializeObject(result);

        // Assert
        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public async Task SortEuropeanCountriesByPopulation_ShouldReturnTopCountriesByPopulation()
    {
        // Arrange   
        var mock = new AutoMocker();
        var countryObject = new List<CountryModel>
        {
            new()
            {
                Name = "Estonia",
                Area = 45227,
                Population = 1331057,
                TopLevelDomain = new List<string> {".ee"},
                NativeName = "Eesti",
                Independent = true
            },
            new()
            {
                Name = "Latvia",
                Area = 64559,
                Population = 1901548,
                TopLevelDomain = new List<string> {".lv"},
                NativeName = "Latvija",
                Independent = true
            }
        };
        mock.GetMock<ICountry>().Setup(country => country.GetCountries()).ReturnsAsync(() => countryObject);

        _countryMock.Setup(x => x.GetCountries())
            .ReturnsAsync(countryObject);

        var expected = countryObject.OrderByDescending(c => c.Population).ToList();

        ICountryService target = new CountryService(mock.GetMock<ICountry>().Object);

        // Act
        var result = await target.SortEuropeanCountriesByPopulation();

        // Assert
        result.Should().Equal(expected);
    }

    [Fact]
    public async Task SortEuropeanCountriesByDensity_ShouldReturnTopCountriesByDensity()
    {
        // Arrange   
        var countryObject = new List<CountryModel>
        {
            new()
            {
                Name = "Greece",
                Area = 131990,
                Population = 10715549,
                TopLevelDomain = new List<string> {".gr"},
                NativeName = "greec",
                Independent = true
            },
            new()
            {
                Name = "Latvia",
                Area = 64559,
                Population = 1901548,
                TopLevelDomain = new List<string> {".lv"},
                NativeName = "Latvija",
                Independent = true
            }
        };
        var mock = new AutoMocker();

        mock.GetMock<ICountry>().Setup(a => a.GetCountries()).ReturnsAsync(() => countryObject);

        var expected = countryObject.OrderByDescending(countryModel => countryModel.Population / countryModel.Area)
            .ToList();

        ICountryService target = new CountryService(mock.GetMock<ICountry>().Object);

        // Act
        var result = await target.SortEuropeanCountriesByDensity();

        // Assert
        result.Should().Equal(expected);
    }

    [Fact]
    public async Task IsEuropeanCountryName_CountryOutsideEurope_ShouldBeFalse()
    {
        // Arrange   
        var countryName = "error";

        var countryObject = new List<CountryModel>
        {
            new()
            {
                Name = "Japan",
                Area = 45212127,
                Population = 121212,
                TopLevelDomain = new List<string> {".jp"},
                NativeName = "jappan",
                Independent = true
            },
            new()
            {
                Name = "Latvia",
                Area = 64559,
                Population = 1901548,
                TopLevelDomain = new List<string> {".lv"},
                NativeName = "Latvija",
                Independent = true
            }
        };

        var mock = new AutoMocker();

        mock.GetMock<ICountry>().Setup(a => a.GetCountries()).ReturnsAsync(() => countryObject);

        ICountryService target = new CountryService(mock.GetMock<ICountry>().Object);

        // Act
        var result = await target.IsEuropeanCountry(countryName);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AllCountries_ShouldReturnAllCountries()
    {
        // Arrange   
        var countryObject = new List<CountryModel>
        {
            new()
            {
                Name = "Japan",
                Area = 45212127,
                Population = 121212,
                TopLevelDomain = new List<string> {".jp"},
                NativeName = "jappan",
                Independent = true
            },
            new()
            {
                Name = "Latvia",
                Area = 64559,
                Population = 1901548,
                TopLevelDomain = new List<string> {".lv"},
                NativeName = "Latvija",
                Independent = true
            }
        };

        var mock = new AutoMocker();

        mock.GetMock<ICountry>().Setup(a => a.GetCountries()).ReturnsAsync(() => countryObject);

        ICountryService target = new CountryService(mock.GetMock<ICountry>().Object);

        // Act
        var actual = await target.AllCountries();

        // Assert
        Assert.Equal(countryObject, actual);
    }

    [Fact]
    public async Task ReturnEuropeUnionCountries_ShouldReturnAllEuropeanCountries()
    {
        // Arrange   
        var countryObject = new List<CountryModel>
        {
            new()
            {
                Name = "Japan",
                Area = 45212127,
                Population = 121212,
                TopLevelDomain = new List<string> {".jp"},
                NativeName = "jappan",
                Independent = false
            },
            new()
            {
                Name = "Latvia",
                Area = 64559,
                Population = 1901548,
                TopLevelDomain = new List<string> {".lv"},
                NativeName = "Latvija",
                Independent = true
            }
        };

        var expected = new List<CountryModel>
        {
            new()
            {
                Name = "Latvia",
                Area = 64559,
                Population = 1901548,
                TopLevelDomain = new List<string> {".lv"},
                NativeName = "Latvija",
                Independent = true
            }
        };

        var mock = new AutoMocker();

        mock.GetMock<ICountry>().Setup(a => a.GetCountries()).ReturnsAsync(() => countryObject);

        ICountryService target = new CountryService(mock.GetMock<ICountry>().Object);

        var actual = await target.ReturnEuropeUnionCountries();

        var expectedString = JsonConvert.SerializeObject(expected);
        var actualString = JsonConvert.SerializeObject(actual);

        Assert.Equal(expectedString, actualString);
    }
}