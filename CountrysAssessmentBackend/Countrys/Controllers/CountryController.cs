using Countrys.Components.CountryService.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CountrysAssessment.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("Country/all")]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var countries = await _countryService.GetAllCountriesAsync();
                if (countries == null)
                {
                    return NotFound();
                }
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching countries.");
            }
        }

        [HttpGet("Country/name/{countryName}")]
        public async Task<IActionResult> GetCountryByName(string countryName)
        {
            try
            {
                var country = await _countryService.GetCountryByNameAsync(countryName);
                if (country == null)
                {
                    return NotFound();
                }
                return Ok(country);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred while fetching the country with country name {countryName}.");
            }
        }

        [HttpGet("Country/code/{countryCode}")]
        public async Task<IActionResult> GetCountryByCode(string countryCode)
        {
            try
            {
                var country = await _countryService.GetCountryByCodeAsync(countryCode);
                if (country == null)
                {
                    return NotFound($"Country with code {countryCode} not found.");
                }
                return Ok(country);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred while fetching the country with code: {countryCode}.");
            }
        }

        [HttpGet("region/{regionName}")]
        public async Task<IActionResult> GetRegionByName(string regionName)
        {
            try
            {
                var region = await _countryService.GetRegionByNameAsync(regionName);
                if (region == null)
                {
                    return NotFound();
                }
                return Ok(region);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the region.");
            }
        }

        [HttpGet("subregion/{subregionName}")]
        public async Task<IActionResult> GetSubregionByName(string subregionName)
        {
            try
            {
                var subregion = await _countryService.GetSubregionByNameAsync(subregionName);
                if (subregion == null)
                {
                    return NotFound();
                }
                return Ok(subregion);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the subregion.");
            }
        }
    }
}