using RHApi.Models;

namespace RHApi.Dtos
{
    public class JobDto
    {
        public string JobTitle { get; set; } = null!;

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public virtual List<EmployeeDto>? Employees { get; set; }
    }
}
