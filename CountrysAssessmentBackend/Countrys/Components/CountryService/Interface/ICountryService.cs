using Countrys.Components.CountryService.Models;

namespace Countrys.Components.CountryService.Interface
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByNameAsync(string name);
        Task<Country> GetCountryByCodeAsync(string code);
        Task<Region> GetRegionByNameAsync(string regionName);
        Task<Subregion> GetSubregionByNameAsync(string subregionName);
    }
}