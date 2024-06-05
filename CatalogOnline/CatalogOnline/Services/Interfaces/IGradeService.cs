using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Services.Interfaces
{
    public interface IGradeService
    {
        List<GradeModel> GetAllGrades();
        GradeModel GetGradeModelById(int gradeId);
        List<GradeModel> GetGradesByCourseId(int courseId);
        List<GradeModel> GetGradesByStudentAndCourse(int studentId, int courseId);
        List<DisplayGradeModel> GetGradesWithCourseDetailsByStudentId(int studentId);


    }
}
