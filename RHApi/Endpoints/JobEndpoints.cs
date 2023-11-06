using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RHApi.Data.Repository;
using RHApi.Dtos;
using RHApi.Models;

namespace RHApi.Endpoints
{
    public static class JobEndpoints
    {
        public static void ConfigureJobEndpoints(this WebApplication app)
        {
            app.MapGet("/jobs", async (IRepository<Job> repository, IMapper mapper) =>
            {
                var jobs = await repository.GetListAsync();
                var result = mapper.Map<List<JobDto>>(jobs);
                return Results.Ok(result);
            })
            .WithName("GetJobs")
            .WithTags("Jobs")
            .WithDescription("Returns a list with all jobs available.")
            .WithOpenApi();

            app.MapGet("/jobs/{id}", async (int id, IRepository<Job> repository, IMapper mapper) =>
            {
                var job = await repository.GetSingleAsync(j => j.JobId == id);
                if (job is null) return Results.NotFound();
                var result = mapper.Map<JobDto>(job);
                return Results.Ok(result);
            })
            .WithName("GetJobById")
            .WithTags("Jobs")
            .WithDescription("Returns the data of a job given a correct Id. Will return 404 if no job with the provided id is found.")
            .WithOpenApi();

            app.MapPost("/jobs", async ([FromBody]JobDto request, IRepository<Job> repository, IMapper mapper, IValidator<JobDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                var job = mapper.Map<Job>(request);
                await repository.CreateAsync(job);
                return Results.StatusCode(201);
            })
            .WithName("Create Job")
            .WithTags("Jobs")
            .WithDescription("Creates a new job in the database. If data provided corresponds to an already existing job, an error response will be returned instead.")
            .WithOpenApi();
        }
    }
}
