﻿using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
namespace CatalogOnline.Mappings
{
    public class CatalogOnlineProfile : Profile
    {
        public CatalogOnlineProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username))
                .ForMember(dest => dest.legal_name, opt => opt.MapFrom(src => src.legal_name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email));
        }
    }
}