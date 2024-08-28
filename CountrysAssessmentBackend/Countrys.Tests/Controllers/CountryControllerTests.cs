using Countrys.Components.CountryService.Interface;
using Countrys.Components.CountryService.Models;
using CountrysAssessment.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Countrys.Tests.Controllers
{
    public class CountryControllerTests
    {
        private readonly Mock<ICountryService> _mockCountryService;
        private readonly CountryController _controller;

        private const string CountryNameTest = "CountryNameTest";
        private const string RegionNameTest = "RegionNameTest";
        private const string SubregionNameTest = "SubregionNameTest";
        private const string CountryCodeTest = "CT1";

        public CountryControllerTests()
        {
            _mockCountryService = new Mock<ICountryService>();
            _controller = new CountryController(_mockCountryService.Object);
        }

        [Fact]
        public async Task GetAllCountries_ReturnsOkResult_WithListOfCountries()
        {
            var countries = new List<Country> { new Country { Name = new Name { Common = CountryNameTest } } };
            _mockCountryService.Setup(service => service.GetAllCountriesAsync()).ReturnsAsync(countries);

            var result = await _controller.GetAllCountries();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Country>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAllCountries_ReturnsNotFound_WhenNoCountries()
        {
            _mockCountryService.Setup(service => service.GetAllCountriesAsync()).ReturnsAsync((List<Country>)null);

            var result = await _controller.GetAllCountries();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCountryByName_ReturnsOkResult_WithCountry()
        {
            var country = new Country { Name = new Name { Common = CountryNameTest } };
            _mockCountryService.Setup(service => service.GetCountryByNameAsync(CountryNameTest)).ReturnsAsync(country);

            var result = await _controller.GetCountryByName(CountryNameTest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Country>(okResult.Value);
            Assert.Equal(CountryNameTest, returnValue.Name.Common);
        }

        [Fact]
        public async Task GetCountryByName_ReturnsNotFound_WhenCountryNotFound()
        {
            _mockCountryService.Setup(service => service.GetCountryByNameAsync(CountryNameTest)).ReturnsAsync((Country)null);

            var result = await _controller.GetCountryByName(CountryNameTest);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCountryByCode_ReturnsOkResult_WithCountry()
        {
            var country = new Country { Name = new Name { Common = CountryNameTest } };
            _mockCountryService.Setup(service => service.GetCountryByCodeAsync(CountryCodeTest)).ReturnsAsync(country);

            var result = await _controller.GetCountryByCode(CountryCodeTest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Country>(okResult.Value);
            Assert.Equal(CountryNameTest, returnValue.Name.Common);
        }

        [Fact]
        public async Task GetCountryByCode_ReturnsNotFound_WhenCountryNotFound()
        {
            _mockCountryService.Setup(service => service.GetCountryByCodeAsync(CountryCodeTest)).ReturnsAsync((Country)null);

            var result = await _controller.GetCountryByCode(CountryCodeTest);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetRegionByName_ReturnsOkResult_WithRegion()
        {
            var region = new Region { Name = RegionNameTest };
            _mockCountryService.Setup(service => service.GetRegionByNameAsync(RegionNameTest)).ReturnsAsync(region);

            var result = await _controller.GetRegionByName(RegionNameTest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Region>(okResult.Value);
            Assert.Equal(RegionNameTest, returnValue.Name);
        }

        [Fact]
        public async Task GetRegionByName_ReturnsNotFound_WhenRegionNotFound()
        {
            _mockCountryService.Setup(service => service.GetRegionByNameAsync(RegionNameTest)).ReturnsAsync((Region)null);

            var result = await _controller.GetRegionByName(RegionNameTest);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetSubregionByName_ReturnsOkResult_WithSubregion()
        {
            var subregion = new Subregion { Name = SubregionNameTest };
            _mockCountryService.Setup(service => service.GetSubregionByNameAsync(SubregionNameTest)).ReturnsAsync(subregion);

            var result = await _controller.GetSubregionByName(SubregionNameTest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Subregion>(okResult.Value);
            Assert.Equal(SubregionNameTest, returnValue.Name);
        }

        [Fact]
        public async Task GetSubregionByName_ReturnsNotFound_WhenSubregionNotFound()
        {
            _mockCountryService.Setup(service => service.GetSubregionByNameAsync(SubregionNameTest)).ReturnsAsync((Subregion)null);

            var result = await _controller.GetSubregionByName(SubregionNameTest);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}