using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CatalogOnline.DAL.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private DataContext _context;

        public EnrollmentRepository(DataContext context)
        {
            _context = context;
        }

        public List<Enrollment> GetEnrollments()
        {
            return _context.Enrollment.OrderBy(e => e.enrollment_id).ToList();
        }
    }
}
