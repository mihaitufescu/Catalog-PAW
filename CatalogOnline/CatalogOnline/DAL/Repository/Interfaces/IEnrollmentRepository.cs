using CatalogOnline.DAL.DBO;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IEnrollmentRepository
    {
        List<Enrollment> GetEnrollments();

    }
}
