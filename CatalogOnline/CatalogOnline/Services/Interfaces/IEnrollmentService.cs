using CatalogOnline.Models;

namespace CatalogOnline.Services.Interfaces
{
    public interface IEnrollmentService
    {
        List<EnrollmentModel> GetAllEnrollments();

    }
}
