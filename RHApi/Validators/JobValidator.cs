using FluentValidation;
using RHApi.Data.Repository;
using RHApi.Dtos;
using RHApi.Models;

namespace RHApi.Validators
{
    public class JobValidator : AbstractValidator<JobDto>
    {
        private readonly IRepository<Job> _repository;
        public JobValidator(IRepository<Job> repository)
        {
            _repository = repository;
            RuleFor(j => j.JobTitle).MinimumLength(3).MaximumLength(50).NotNull();
            RuleFor(j => j.MinSalary).GreaterThan(100).LessThan(j => j.MaxSalary);
            RuleFor(j => j.MaxSalary).GreaterThan(j => j.MinSalary).LessThan(1000000);
            RuleFor(j => j).Must(JobDoesNotExist).WithName("JobExists").WithMessage("A job with that description already exists in the system.");
        }

        private bool JobDoesNotExist(JobDto job)
        {
            return _repository.GetSingleAsync(j => j.JobTitle == job.JobTitle).Result is null;
        }
    }
}
