using Countrys.Components.CountryService.Interface;
using Countrys.Components.CountryService.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Countrys.Components.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly IMemoryCache _cache;
        private readonly IWebCommunicatorBroker _webCommunicatorBroker;
        private const string BaseUrl = "https://restcountries.com/v3.1";

        public CountryService(IMemoryCache cache, IWebCommunicatorBroker webCommunicatorBroker)
        {
            _cache = cache;
            _webCommunicatorBroker = webCommunicatorBroker;
        }

        /// <summary>
        /// Retrieves a list of all countries, with caching.
        /// </summary>
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            try
            {
                if (!_cache.TryGetValue("countries", out List<Country> countries))
                {
                    var fullUrl = $"{BaseUrl}/all";
                    countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                    if (countries == null)
                    {
                        throw new Exception("Country list returned null.");
                    }

                    _cache.Set("countries", countries, TimeSpan.FromHours(1));
                }
                return countries.OrderBy(c => c.Name.Common).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred retrieving Countries List.", ex);
            }
        }

        /// <summary>
        /// Retrieves a country by its name, with caching.
        /// </summary>
        /// <param name="name">The name of the country.</param>
        public async Task<Country> GetCountryByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Country name cannot be null or empty.");
            }

            try
            {
                if (!_cache.TryGetValue($"country_name_{name}", out Country country))
                {
                    string fullUrl = $"{BaseUrl}/name/{name}";
                    var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                    country = countries.FirstOrDefault() ?? throw new KeyNotFoundException($"No countries found with the name {name}.");

                    _cache.Set($"country_name_{name}", country, TimeSpan.FromHours(1));
                }
                return country;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the country with name {name}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a country by its code, with caching.
        /// </summary>
        /// <param name="code">The code of the country.</param>
        public async Task<Country> GetCountryByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code), "Country code cannot be null or empty.");
            }

            try
            {
                if (!_cache.TryGetValue($"country_code_{code}", out Country country))
                {
                    string fullUrl = $"{BaseUrl}/alpha/{code}";
                    var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                    country = countries.FirstOrDefault() ?? throw new KeyNotFoundException($"No countries found with the code {code}.");

                    _cache.Set($"country_code_{code}", country, TimeSpan.FromHours(1));
                }
                return country;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the country with code {code}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a region by its name, with caching.
        /// </summary>
        /// <param name="regionName">The name of the region.</param>
        public async Task<Region> GetRegionByNameAsync(string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionName))
            {
                throw new ArgumentNullException(nameof(regionName), "Region name cannot be null or empty.");
            }

            try
            {
                if (!_cache.TryGetValue($"region_name_{regionName}", out Region region))
                {
                    string fullUrl = $"{BaseUrl}/region/{regionName}";

                    var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                    var subregions = countries.Select(c => c.Subregion).Distinct().ToList();
                    var population = countries.Sum(c => c.Population);

                    region = new Region
                    {
                        Name = regionName,
                        Population = population,
                        Countries = countries.Select(c => c.Name.Common).ToList(),
                        Subregions = subregions
                    };

                    _cache.Set($"region_name_{regionName}", region, TimeSpan.FromHours(1));
                }
                return region;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the region with name {regionName}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a subregion by its name, with caching.
        /// </summary>
        /// <param name="subregionName">The name of the subregion.</param>
        public async Task<Subregion> GetSubregionByNameAsync(string subregionName)
        {
            if (string.IsNullOrWhiteSpace(subregionName))
            {
                throw new ArgumentNullException(nameof(subregionName), "Subregion name cannot be null or empty.");
            }

            try
            {
                if (!_cache.TryGetValue($"subregion_name_{subregionName}", out Subregion subregion))
                {
                    string fullUrl = $"{BaseUrl}/region/{subregionName}";

                    var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                    var region = countries.FirstOrDefault()?.Region;
                    var population = countries.Sum(c => c.Population);

                    subregion = new Subregion
                    {
                        Name = subregionName,
                        Population = population,
                        Region = region,
                        Countries = countries.Select(c => c.Name.Common).ToList()
                    };

                    _cache.Set($"subregion_name_{subregionName}", subregion, TimeSpan.FromHours(1));
                }
                return subregion;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the subregion with name {subregionName}.", ex);
            }
        }

        /// <summary>
        /// Helper method to get data from the web broker.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="url">The URL to retrieve data from.</param>
        private async Task<T> GetDataFromWebBrokerAsync<T>(string url)
        {
            var response = await _webCommunicatorBroker.GetAsync(url);
            var data = JsonConvert.DeserializeObject<T>(response);
            if (data == null)
            {
                throw new InvalidOperationException("The response data is null.");
            }
            return data;
        }
    }
}