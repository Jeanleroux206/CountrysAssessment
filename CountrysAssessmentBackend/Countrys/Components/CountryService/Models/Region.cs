namespace Countrys.Components.CountryService.Models
{
    public class Region
    {
        public string Name { get; set; } = string.Empty;
        public long Population { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Subregions { get; set; }
    }
}
