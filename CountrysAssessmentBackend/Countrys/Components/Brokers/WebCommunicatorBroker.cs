using Countrys.Components.CountryService.Interface;

namespace Countrys.Components.Brokers
{
    public class WebCommunicatorBroker : IWebCommunicatorBroker
    {
        private readonly HttpClient _httpClient;

        public WebCommunicatorBroker(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching data from {url}", ex);
            }
        }
    }
}