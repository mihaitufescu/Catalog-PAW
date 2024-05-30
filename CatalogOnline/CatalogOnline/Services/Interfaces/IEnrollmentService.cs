using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;

namespace CatalogOnline.Services.Interfaces
{
    public interface IEnrollmentService
    {
        List<EnrollmentModel> GetAllEnrollments();

        EnrollmentModel GetEnrollmentById(int id);

        List<StudentCourseModel> GetStudentCoursesWithGrades(int studentId);


    }
}
