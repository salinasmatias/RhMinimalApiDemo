namespace RHApi.Dtos
{
    public class LocationDto
    {
        public string? StreetAddress { get; set; }

        public string? PostalCode { get; set; }

        public string City { get; set; } = null!;

        public string? StateProvince { get; set; }
    }
}
