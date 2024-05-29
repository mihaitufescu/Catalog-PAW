using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext context)
        {
            _context = context;
        }

        public List<Grade> GetGrades()
        {
            return _context.Grade.OrderBy(p => p.grade_id).ToList();
        }

        public Grade GetGradeById(int id)
        {
            return _context.Grade.Find(id);
        }

        public List<Grade> GetGradesByUserId(int userId)
        {
            return _context.Grade.Where(g => g.user_id == userId).ToList();
        }

        public List<Grade> GetGradesByCourseId(int courseId)
        {
            return _context.Grade.Where(g => g.course_id == courseId).ToList();
        }

        public async Task<bool> AddGrade(Grade grade)
        {
            _context.Grade.Add(grade);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateGrade(Grade grade)
        {
            var existingGrade = _context.Grade.Local.FirstOrDefault(g => g.grade_id == grade.grade_id);
            if (existingGrade != null)
            {
                _context.Entry(existingGrade).State = EntityState.Detached;
            }

            _context.Grade.Update(grade);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGrade(int id)
        {
            var grade = await _context.Grade.FindAsync(id);
            if (grade != null)
            {
                _context.Grade.Remove(grade);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public List<Grade> GetGradesByStudentId(int studentId)
        {
            return _context.Grade.Where(g => g.user_id == studentId).ToList();
        }

        public List<Grade> GetGradesByStudentIdAndCourseId(int studentId, int courseId)
        {
            return _context.Grade
                .Where(g => g.user_id == studentId && g.course_id == courseId)
                .ToList();
        }

        void IGradeRepository.DeleteGrade(int id)
        {
            var grade = _context.Grade.Find(id);
            if (grade != null)
            {
                _context.Grade.Remove(grade);
                _context.SaveChanges();
            }
        }


    }
}
