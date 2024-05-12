using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.DAL.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private DataContext _context;

        public GradeRepository(DataContext context)
        {
            _context = context;
        }

        public List<Grade> GetGrades()
        {
            return _context.Grade.OrderBy(p => p.grade_id).ToList();
        }
    }
}
