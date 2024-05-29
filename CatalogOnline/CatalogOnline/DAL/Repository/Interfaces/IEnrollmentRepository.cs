using CatalogOnline.DAL.DBO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IEnrollmentRepository
    {
        List<Enrollment> GetEnrollments();

        Enrollment GetEnrollmentById(int id);

        Task<bool> AddEnrollment(Enrollment enrollment);

        void DeleteEnrollment(int id);

        Task<bool> UpdateEnrollment(Enrollment enrollment);
    }
}
