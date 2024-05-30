using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CatalogOnline.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public CourseService(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public List<CourseModel> GetAllCourseModels()
        {
            var courses = _courseRepository.GetAllCourses();
            return _mapper.Map<List<CourseModel>>(courses); 
        }


        public List<CourseModel> GetCoursesBySubject(string subject)
        {
            var courses = _courseRepository.GetCoursesBySubject(subject);
            return _mapper.Map<List<CourseModel>>(courses);
        }

        public List<CourseModel> GetCoursesByYear(int year)
        {
            var courses = _courseRepository.GetCoursesByYear(year);
            return _mapper.Map<List<CourseModel>>(courses);
        }



    }
}
