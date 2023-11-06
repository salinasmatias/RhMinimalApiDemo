using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RHApi.Data;
using RHApi.Data.Repository;
using RHApi.Dtos;
using RHApi.Models;
using RHApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
builder.Services.AddDbContext<HrDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IValidator<JobDto>, JobValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.MapGet("/countries", async (IRepository<Country> repository, IMapper mapper) =>
{
    var countries = await repository.GetListAsync(c => c.Region);
    var result = mapper.Map<List<CountryDto>>(countries);
    return Results.Ok(result);
})
.WithName("GetCountries")
.WithDescription("Returns a list with all countries available")
.WithTags("Countries")
.WithOpenApi();

app.MapGet("/countries/{id}", async (string id, IRepository<Country> repository, IMapper mapper) =>
{
    var country = await repository.GetSingleAsync(c => c.CountryId == id, c => c.Region);
    if (country == null) return Results.NotFound();
    var result = mapper.Map<CountryDto>(country);
    return Results.Ok(result);
})
.WithName("GetCountryById")
.WithTags("Countries")
.WithDescription("Returns the data of a country given a correct Id. Will return a 404 if no country with the provided id is found")
.WithOpenApi();

app.MapGet("/jobs", async (IRepository<Job> repository, IMapper mapper) =>
{
    var jobs = await repository.GetListAsync();
    var result = mapper.Map<List<JobDto>>(jobs);
    return Results.Ok(result);
})
.WithName("GetJobs")
.WithTags("Jobs")
.WithDescription("Returns a list with all jobs available")
.WithOpenApi();

app.MapGet("/jobs/{id}", async (int id, IRepository<Job> repository, IMapper mapper) =>
{
    var job = await repository.GetSingleAsync(j => j.JobId == id);
    if(job is null) return Results.NotFound();
    var result = mapper.Map<JobDto>(job);
    return Results.Ok(result);
})
.WithName("GetJobById")
.WithTags("Jobs")
.WithDescription("Returns the data of a job given a correct Id. Will return 404 if no job with the provided id is found")
.WithOpenApi();

app.MapPost("/jobs", async (JobDto request, IRepository<Job> repository, IMapper mapper, IValidator<JobDto> validator) =>
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
.WithDescription("Creates a new job in the database")
.WithOpenApi();


app.Run();

public partial class Program { }