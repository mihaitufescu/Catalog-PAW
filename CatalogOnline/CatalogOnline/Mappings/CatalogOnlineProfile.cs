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

            CreateMap<Course, CourseModel>()
                .ForMember(dest => dest.subject, opt => opt.MapFrom(src => src.subject))
                .ForMember(dest => dest.mandatory, opt => opt.MapFrom(src => src.mandatory))
                .ForMember(dest => dest.credits_number, opt => opt.MapFrom(src => src.credits_number))
                .ForMember(dest => dest.year, opt => opt.MapFrom(src => src.year));

            CreateMap<Grade, GradeModel>()
                .ForMember(dest => dest.score, opt => opt.MapFrom(src => src.score))
                .ForMember(dest => dest.type_of_exam, opt => opt.MapFrom(src => src.type_of_exam))
                .ForMember(dest => dest.percentage, opt => opt.MapFrom(src => src.percentage));

            CreateMap<Enrollment, EnrollmentModel>()
                .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.course_id, opt => opt.MapFrom(src => src.course_id))
                .ForMember(dest => dest.joined_since, opt => opt.MapFrom(src => src.joined_since));

        }
    }
}
