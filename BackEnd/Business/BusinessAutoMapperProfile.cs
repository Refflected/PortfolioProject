using AutoMapper;
using Business.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessAutoMapperProfile : Profile
    {
        public BusinessAutoMapperProfile()
        {
            CreateMap<User, UserBusinessDto>();
        }
    }
}
