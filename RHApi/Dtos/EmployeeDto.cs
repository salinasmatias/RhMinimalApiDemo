namespace RHApi.Dtos
{
    public class EmployeeDto
    {
        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
    }
}
