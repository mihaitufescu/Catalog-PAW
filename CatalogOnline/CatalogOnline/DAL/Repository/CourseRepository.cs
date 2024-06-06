using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CatalogOnline.DAL.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            return _context.Course.OrderBy(p => p.course_id).ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Course.FirstOrDefault(c => c.course_id == courseId);
        }

        public List<Course> GetCoursesBySubject(string subject)
        {
            return _context.Course.Where(c => c.subject.ToLower().Contains(subject.ToLower())).ToList();
        }

        public List<Course> GetCoursesByYear(int year)
        {
            return _context.Course.Where(c => c.year == year).ToList();
        }
        
        public async Task<bool> AddCourse(Course course)
        {
            _context.Course.Add(course);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public void DeleteCourse(int courseId)
        {
            var course = _context.Course.Find(courseId);
            if (course != null)
            {
                _context.Course.Remove(course);
                _context.SaveChanges();
            }
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            var existingCourse = _context.Course.Local.FirstOrDefault(c => c.course_id == course.course_id);
            if (existingCourse != null)
            {
                _context.Entry(existingCourse).State = EntityState.Detached;
            }

            _context.Course.Update(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public Course FindCourseByName(string courseName)
        {
            return _context.Course.FirstOrDefault(c => c.subject == courseName);
        }
    }
}
