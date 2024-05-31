using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;

namespace CatalogOnline.Services.Interfaces
{
    public interface IEnrollmentService
    {
        List<EnrollmentModel> GetAllEnrollments();

        EnrollmentModel GetEnrollmentById(int id);

        List<StudentCourseModel> GetStudentCoursesWithGrades(int studentId);

        Task<List<StudentCourseModel>> GetAllCoursesAsync(int userId);
        Task<List<StudentCourseModel>> GetCoursesByYearAsync(int userId, int year);
    }
}
