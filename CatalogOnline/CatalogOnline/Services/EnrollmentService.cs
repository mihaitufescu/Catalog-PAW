using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogOnline.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly INotificationRepository _notificationRepository;

        public EnrollmentService(IMapper mapper, IEnrollmentRepository enrollmentRepository,
            ICourseRepository courseRepository, IUserRepository userRepository, IGradeRepository gradeRepository, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _gradeRepository = gradeRepository;
            _notificationRepository = notificationRepository;


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

      

        public List<StudentCourseModel> GetStudentCoursesWithGrades(int studentId)
        {
            var enrollments = _enrollmentRepository.GetEnrollments().Where(e => e.user_id == studentId).ToList();
            var courseIds = enrollments.Select(e => e.course_id).ToList();
            var courses = _courseRepository.GetAllCourses().Where(c => courseIds.Contains(c.course_id)).ToList();
            var student = _userRepository.GetUserById(studentId);

            var studentCourses = enrollments.Join(
                courses,
                enrollment => enrollment.course_id,
                course => course.course_id,
                (enrollment, course) => new StudentCourseModel
                {
                    CourseId = course.course_id,
                    CourseName = course.subject,
                    Credits = course.credits_number,
                    Semester = enrollment.semester,
                    Year = course.year,
                    Mandatory = course.mandatory,
                    Group = student?.group
                }).ToList();

            return studentCourses;
        }

        public async Task<List<StudentCourseModel>> GetAllCoursesAsync(int userId)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByUserAsync(userId);
            return enrollments.Select(e => new StudentCourseModel
            {
                CourseId = e.Course.course_id,
                CourseName = e.Course.subject,
                Credits = e.Course.credits_number,
                Semester = e.semester,
                Year = e.Course.year,
                Mandatory = e.Course.mandatory,
                Group = null 
            }).OrderBy(c => c.Semester).ToList();
        }

        public async Task<List<StudentCourseModel>> GetCoursesByYearAsync(int userId, int year)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByUserAsync(userId);
            var filteredEnrollments = enrollments.Where(e => e.Course.year == year);

            var coursesWithGrades = filteredEnrollments.Select(async e => {
                var courseModel = _mapper.Map<StudentCourseModel>(e.Course);
                var grades = _gradeRepository.GetGradesByStudentAndCourse(userId, e.course_id);
                courseModel.Grades = grades.Select(g => new GradeDetailModel
                {
                    Score = g.score,
                    TypeOfExam = g.type_of_exam,
                    Percentage = g.percentage
                }).ToList();
                return courseModel;
            }).ToList();

            return (await Task.WhenAll(coursesWithGrades)).ToList();
        }


    }
}
