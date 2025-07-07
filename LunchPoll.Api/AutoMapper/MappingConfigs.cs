using AutoMapper;
using LunchPoll.Api.Data;
using LunchPoll.Api.Data.Dtos;

namespace LunchPoll.Api.AutoMapper
{
    public class MappingConfigs : Profile
    {
        public MappingConfigs()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
        }
    }
}
