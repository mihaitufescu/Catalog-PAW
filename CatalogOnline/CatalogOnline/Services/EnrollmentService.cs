using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;

namespace CatalogOnline.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(IMapper mapper, IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
        }

        public List<EnrollmentModel> GetAllEnrollments()
        {
            var enrollments = _enrollmentRepository.GetEnrollments();
            return _mapper.Map<List<EnrollmentModel>>(enrollments);
        }

        public EnrollmentModel GetEnrollmentById(int id)
        {
            var enrollment = _enrollmentRepository.GetEnrollmentById(id);
            return _mapper.Map<EnrollmentModel>(enrollment);
        }

        //To rewrite this.
        public List<StudentCourseModel> GetStudentCoursesWithGrades(int studentId)
        {
            var enrollments = _enrollmentRepository.GetEnrollments().Where(e => e.user_id == studentId).ToList();
            var courseIds = enrollments.Select(e => e.course_id).ToList();
            var courses = _courseRepository.GetAllCourses().Where(c => courseIds.Contains(c.course_id)).ToList();

            var studentCourses = enrollments.Join(
                courses,
                enrollment => enrollment.course_id,
                course => course.course_id,
                (enrollment, course) => new StudentCourseModel
                {
                    CourseId = course.course_id,
                    CourseName = course.subject,
                    Credits = course.credits_number,
                    DateJoined = enrollment.joined_since
                }).ToList();

            return studentCourses;
        }
    }
}
