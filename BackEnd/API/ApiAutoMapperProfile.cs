using API.DTOs;
using AutoMapper;
using Business.Dtos;

namespace API
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<UserBusinessDto, UserApiDto>();
        }
    }
}
