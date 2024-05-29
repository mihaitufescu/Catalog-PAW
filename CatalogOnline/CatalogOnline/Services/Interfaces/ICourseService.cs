using CatalogOnline.Models;
using System.Collections.Generic;
using CatalogOnline.DAL.DBO;
namespace CatalogOnline.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseModel> GetAllCourses();
        List<CourseModel> GetCoursesBySubject(string subject);
        List<CourseModel> GetCoursesByYear(int year);
        Course GetCourseById(int courseId);

        Task<bool> AddCourse(Course course);
        Task<bool> UpdateCourse(Course course);
        void DeleteCourse(int courseId);


        
        /*
        void AddCourse(CourseModel course);
        void UpdateCourse(CourseModel course);
        void DeleteCourse(int courseId);
        */
    }
}
