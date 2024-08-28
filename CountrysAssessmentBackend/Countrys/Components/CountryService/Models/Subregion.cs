namespace Countrys.Components.CountryService.Models
{
    public class Subregion
    {
        public string Name { get; set; } = string.Empty;
        public long Population { get; set; }
        public string Region { get; set; } = string.Empty;
        public List<string> Countries { get; set; }
    }
}