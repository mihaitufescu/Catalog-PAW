using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly DataContext _context;

        public EnrollmentRepository(DataContext context)
        {
            _context = context;
        }

        public List<Enrollment> GetEnrollments()
        {
            return _context.Enrollment.OrderBy(e => e.enrollment_id).ToList();
        }

        public Enrollment GetEnrollmentById(int id)
        {
            return _context.Enrollment.Find(id);
        }

        public Enrollment GetEnrollmentByUserAndCourseID(int userId, int course)
        {
            return _context.Enrollment.FirstOrDefault(e => e.user_id == userId && e.course_id == course);
        }

        public async Task<bool> AddEnrollment(Enrollment enrollment)
        {
            _context.Enrollment.Add(enrollment);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public void DeleteEnrollment(int id)
        {
            var enrollment = _context.Enrollment.Find(id);
            if (enrollment != null)
            {
                _context.Enrollment.Remove(enrollment);
                _context.SaveChanges();
            }
            
        }

         

        public async Task<bool> UpdateEnrollment(Enrollment enrollment)
        {
            var existingEnrollment = await _context.Enrollment.FindAsync(enrollment.enrollment_id);
            if (existingEnrollment != null)
            {
                _context.Entry(existingEnrollment).State = EntityState.Detached;
                _context.Enrollment.Update(enrollment);
                var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            return false;
        }

        public async Task<List<Enrollment>> GetEnrollmentsByUserAsync(int userId)
        {
            return await _context.Enrollment
                .Include(e => e.Course)
                .Where(e => e.user_id == userId)
                .OrderBy(e => e.semester)
                .ToListAsync();
        }
    }
}
