namespace Countrys.Components.CountryService.Interface
{
    public interface IWebCommunicatorBroker
    {
        Task<string> GetAsync(string url);
    }
}