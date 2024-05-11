using CatalogOnline.Models;
using System.Collections.Generic;

namespace CatalogOnline.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseModel> GetAllCourses();
        List<CourseModel> GetCoursesBySubject(string subject);
        List<CourseModel> GetCoursesByYear(int year);
        
        /*
        void AddCourse(CourseModel course);
        void UpdateCourse(CourseModel course);
        void DeleteCourse(int courseId);
        */
    }
}
