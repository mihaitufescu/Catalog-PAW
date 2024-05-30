using CatalogOnline.DAL.DBO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IGradeRepository
    {
        List<Grade> GetGrades();
        Grade GetGradeById(int id);
        List<Grade> GetGradesByUserId(int userId);
        List<Grade> GetGradesByCourseId(int courseId);
        List<Grade> GetGradesByStudentId(int studentId);
        List<Grade> GetGradesByStudentAndCourse(int studentId, int courseId);
        void AddGrade(Grade grade);
        void UpdateGrade(Grade grade);
        void DeleteGrade(int id);

        Task<bool> DeleteGradeAsync(int id);

    }
}
