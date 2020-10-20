using APP.DOMAIN;
using APP.DOMAIN.Identity;
using APP.WEBAPI.DTOs;
using AutoMapper;

namespace APP.WEBAPI.Helpers
{
    public class helpers : Profile
    {
//public AutoMapperProfilles()
        public helpers()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}