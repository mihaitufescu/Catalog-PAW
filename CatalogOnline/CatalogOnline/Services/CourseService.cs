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

        public List<CourseModel> GetAllCourses()
        {
            var courses = _courseRepository.GetAllCourses();
            return _mapper.Map<List<CourseModel>>(courses); 
        }


        public CourseModel GetCourseById(int courseId)
        {
            var course = _courseRepository.GetCourseById(courseId);
            return _mapper.Map<CourseModel>(course);
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

        /*
        public void AddCourse(CourseModel courseModel)
        {
            var course = _mapper.Map<Course>(courseModel);
            _courseRepository.AddCourse(course);
        }

        public void UpdateCourse(CourseModel courseModel)
        {
            var course = _mapper.Map<Course>(courseModel);
            _courseRepository.UpdateCourse(course);
        }

        public void DeleteCourse(int courseId)
        {
            _courseRepository.DeleteCourse(courseId);
        }
        */
    }
}
