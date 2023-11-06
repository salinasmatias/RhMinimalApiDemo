using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RHApi.Data;
using RHApi.Data.Repository;
using RHApi.Dtos;
using RHApi.Endpoints;
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
app.ConfigureCountryEndpoints();
app.ConfigureJobEndpoints();

app.Run();

public partial class Program { }