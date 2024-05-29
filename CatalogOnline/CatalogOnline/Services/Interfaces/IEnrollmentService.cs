using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;

namespace CatalogOnline.Services.Interfaces
{
    public interface IEnrollmentService
    {
        List<EnrollmentModel> GetAllEnrollments();

        EnrollmentModel GetEnrollmentById(int id);

        bool AddEnrollmentModel(EnrollmentModel enrollment);

        bool AddEnrollment(Enrollment enrollment);

        bool UpdateEnrollment(Enrollment enrollment);

        void DeleteEnrollment(int id);


    }
}
