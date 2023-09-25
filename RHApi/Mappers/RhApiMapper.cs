using AutoMapper;
using RHApi.Dtos;
using RHApi.Models;

namespace RHApi.Mappers
{
    public class RhApiMapper : Profile
    {
        public RhApiMapper()
        {
            CreateMap<Country, CountryDto>().ForMember(x => x.Region, option => option.MapFrom(source => source.Region.RegionName));
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
