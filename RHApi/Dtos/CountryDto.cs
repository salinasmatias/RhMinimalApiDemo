using RHApi.Models;

namespace RHApi.Dtos
{
    public class CountryDto
    {
        public string? CountryName { get; set; }
        public string  Region { get; set; } = null!;
    }
}
