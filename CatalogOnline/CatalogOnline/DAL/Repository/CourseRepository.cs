using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
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
            return _context.Course.ToList();
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
        /*
        public void AddCourse(Course course)
        {
            _context.Course.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _context.Course.Update(course);
            _context.SaveChanges();
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
        */
    }
}
