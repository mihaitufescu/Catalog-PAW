using CatalogOnline.DAL.DBO;
using System.Collections.Generic;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        List<Course> GetCoursesBySubject(string subject);
        List<Course> GetCoursesByYear(int year);
       /*
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
       */
        
    }
}
