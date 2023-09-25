using RHApi.Models;

namespace RHApi.Dtos
{
    public class CountryDto
    {
        public string? CountryName { get; set; }
        public virtual List<LocationDto>? Locations { get; set; }
        public virtual RegionDto Region { get; set; } = null!;
    }
}
