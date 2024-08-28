using Newtonsoft.Json;

namespace Countrys.Components.CountryService.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("subregion")]
        public string Subregion { get; set; } = string.Empty;
        [JsonProperty("capital")]
        public List<string> Capital { get; set; }
        [JsonProperty("currencies")]
        public Dictionary<string, Currency> Currencies { get; set; }
        [JsonProperty("languages")]
        public Dictionary<string, string> Languages { get; set; }
        [JsonProperty("borders")]
        public List<string> Borders { get; set; }
    }
    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
    public class Currency
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}