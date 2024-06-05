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
    public class GradeService : IGradeService
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;
        private readonly ICourseRepository _courseRepository;

        public GradeService(IMapper mapper, IGradeRepository gradeRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
        }

        public List<GradeModel> GetAllGrades()
        {
            var grades = _gradeRepository.GetGrades();
            return _mapper.Map<List<GradeModel>>(grades);
        }

        public GradeModel GetGradeModelById(int id)
        {
            var grade = _gradeRepository.GetGradeById(id);
            return _mapper.Map<GradeModel>(grade);
        }

        public List<GradeModel> GetGradesByCourseId(int courseId)
        {
            var grades = _gradeRepository.GetGradesByCourseId(courseId);
            return _mapper.Map<List<GradeModel>>(grades);
        }

        public List<GradeModel> GetGradesByStudentAndCourse(int studentId, int courseId)
        {
            var grades = _gradeRepository.GetGradesByStudentAndCourse(studentId, courseId);
            return _mapper.Map<List<GradeModel>>(grades);
        }

        public List<DisplayGradeModel> GetGradesWithCourseDetailsByStudentId(int studentId)
        {
            var grades = _gradeRepository.GetGradesByStudentId(studentId);

            var displayGrades = new List<DisplayGradeModel>();

            foreach (var grade in grades)
            {
                var displayGrade = new DisplayGradeModel
                {
                    grade_id = grade.grade_id,
                    score = grade.score,
                    type_of_exam = grade.type_of_exam,
                    percentage = grade.percentage
                };

                // Get course details by course ID
                var course = _courseRepository.GetCourseById(grade.course_id);
                if (course != null)
                {
                    displayGrade.course_name = course.subject;
                    displayGrade.year = course.year;
                }

                displayGrades.Add(displayGrade);
            }

            return displayGrades;
        }
    }
}
