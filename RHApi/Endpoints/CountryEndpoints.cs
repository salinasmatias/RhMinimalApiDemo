using AutoMapper;
using RHApi.Data.Repository;
using RHApi.Dtos;
using RHApi.Models;

namespace RHApi.Endpoints
{
    public static class CountryEndpoints
    {
        public static void ConfigureCountryEndpoints(this WebApplication app)
        {
            app.MapGet("/countries", async (IRepository<Country> repository, IMapper mapper) =>
            {
                var countries = await repository.GetListAsync(c => c.Region);
                var result = mapper.Map<List<CountryDto>>(countries);
                return Results.Ok(result);
            })
            .WithName("GetCountries")
            .WithDescription("Returns a list with all countries available.")
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
            .WithDescription("Returns the data of a country given a correct Id. Will return a 404 if no country with the provided id is found.")
            .WithOpenApi();
        }
    }
}
