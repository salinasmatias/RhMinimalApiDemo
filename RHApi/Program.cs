using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RHApi.Data;
using RHApi.Dtos;
using RHApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
builder.Services.AddDbContext<HrDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.MapGet("/weatherforecast", (HrDbContext context, IMapper mapper) =>
{
    var dbTest = context.Countries.Include(x => x.Region).ToList();
    var result = mapper.Map<List<CountryDto>>(dbTest);
    return result;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

public partial class Program { }