using AutoMapper;
using Company.Models.Dtos;
using Company.Models.v1;

namespace Company.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Developer, DeveloperDto>().ReverseMap();
        }
    }
}
