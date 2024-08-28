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

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Country name cannot be null or empty.");
            }

            try
            {
                string fullUrl = $"{BaseUrl}/name/{name}";
                var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                return countries.FirstOrDefault() ?? throw new KeyNotFoundException($"No countries found with the name {name}.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the country with name {name}.", ex);
            }
        }

        public async Task<Country> GetCountryByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code), "Country code cannot be null or empty.");
            }

            try
            {
                string fullUrl = $"{BaseUrl}/alpha/{code}";
                var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                return countries.FirstOrDefault() ?? throw new KeyNotFoundException($"No countries found with the code {code}.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the country with code {code}.", ex);
            }
        }

        public async Task<Region> GetRegionByNameAsync(string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionName))
            {
                throw new ArgumentNullException(nameof(regionName), "Region name cannot be null or empty.");
            }

            try
            {
                string fullUrl = $"{BaseUrl}/region/{regionName}";

                var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                var subregions = countries.Select(c => c.Subregion).Distinct().ToList();
                var population = countries.Sum(c => c.Population);

                return new Region
                {
                    Name = regionName,
                    Population = population,
                    Countries = countries.Select(c => c.Name.Common).ToList(),
                    Subregions = subregions
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the region with name {regionName}.", ex);
            }
        }

        public async Task<Subregion> GetSubregionByNameAsync(string subregionName)
        {
            if (string.IsNullOrWhiteSpace(subregionName))
            {
                throw new ArgumentNullException(nameof(subregionName), "Subregion name cannot be null or empty.");
            }

            try
            {
                string fullUrl = $"{BaseUrl}/region/{subregionName}";

                var countries = await GetDataFromWebBrokerAsync<List<Country>>(fullUrl);
                var region = countries.FirstOrDefault()?.Region;
                var population = countries.Sum(c => c.Population);

                return new Subregion
                {
                    Name = subregionName,
                    Population = population,
                    Region = region,
                    Countries = countries.Select(c => c.Name.Common).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the subregion with name {subregionName}.", ex);
            }
        }

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