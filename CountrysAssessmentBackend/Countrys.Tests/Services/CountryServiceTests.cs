using Countrys.Components.CountryService;
using Countrys.Components.CountryService.Interface;
using Countrys.Components.CountryService.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Countrys.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly Mock<IWebCommunicatorBroker> _mockWebCommunicatorBroker;
        private readonly CountryService _countryService;

        private const string CountryNameTest = "CountryNameTest";
        private const string RegionNameTest = "RegionNameTest";
        private const string SubregionNameTest = "SubregionNameTest";
        private const string CountryCodeTest = "C1";

        public CountryServiceTests()
        {
            _mockCache = new Mock<IMemoryCache>();
            _mockWebCommunicatorBroker = new Mock<IWebCommunicatorBroker>();

            _mockCache.Setup(m => m.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
                      .Returns(false);

            var mockCacheEntry = new Mock<ICacheEntry>();
            _mockCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                      .Returns(mockCacheEntry.Object);
            _countryService = new CountryService(_mockCache.Object, _mockWebCommunicatorBroker.Object);
        }

        private void SetupMockResponse<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            _mockWebCommunicatorBroker.Setup(broker => broker.GetAsync(It.IsAny<string>())).ReturnsAsync(jsonData);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsListOfCountries()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Name = new Name { Common = CountryNameTest },
                    Population = It.IsAny<int>(),
                    Region = It.IsAny < string >(),
                    Subregion = It.IsAny<string>()
                }
            };
            SetupMockResponse(countries);

            var result = await _countryService.GetAllCountriesAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(CountryNameTest, result[0].Name.Common);
        }

        [Fact]
        public async Task GetCountryByNameAsync_ReturnsCountry()
        {
            var country = new Country { Name = new Name { Common = CountryNameTest } };
            SetupMockResponse(new List<Country> { country });

            var result = await _countryService.GetCountryByNameAsync(CountryNameTest);

            Assert.NotNull(result);
            Assert.Equal(CountryNameTest, result.Name.Common);
        }

        [Fact]
        public async Task GetCountryByCodeAsync_ReturnsCountry()
        {
            var country = new Country { Name = new Name { Common = CountryNameTest } };
            SetupMockResponse(new List<Country> { country });

            var result = await _countryService.GetCountryByCodeAsync(CountryCodeTest);

            Assert.NotNull(result);
            Assert.Equal(CountryNameTest, result.Name.Common);
        }

        [Fact]
        public async Task GetRegionByNameAsync_ReturnsRegion()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Name = new Name { Common = CountryNameTest },
                    Population = It.IsAny<int>(),
                    Region = It.IsAny < string >(),
                    Subregion = It.IsAny<string>()
                }
            };
            SetupMockResponse(countries);

            var result = await _countryService.GetRegionByNameAsync(RegionNameTest);

            Assert.NotNull(result);
            Assert.Equal(RegionNameTest, result.Name);
        }

        [Fact]
        public async Task GetSubregionByNameAsync_ReturnsSubregion()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Name = new Name { Common = CountryNameTest },
                    Population = It.IsAny<int>(),
                    Region = It.IsAny < string >(),
                    Subregion = It.IsAny<string>()
                }
            };
            SetupMockResponse(countries);

            var result = await _countryService.GetSubregionByNameAsync(SubregionNameTest);

            Assert.NotNull(result);
            Assert.Equal(SubregionNameTest, result.Name);
        }
    }
}