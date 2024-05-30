using CatalogOnline.Models;
using System.Collections.Generic;
using CatalogOnline.DAL.DBO;
namespace CatalogOnline.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseModel> GetAllCourseModels();
        List<CourseModel> GetCoursesBySubject(string subject);
        List<CourseModel> GetCoursesByYear(int year);

    }
}
