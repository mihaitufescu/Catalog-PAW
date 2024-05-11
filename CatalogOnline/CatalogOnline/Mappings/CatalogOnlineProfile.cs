using AutoMapper;
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

        }
    }
}
