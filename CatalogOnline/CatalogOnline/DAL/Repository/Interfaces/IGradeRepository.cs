using CatalogOnline.DAL.DBO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IGradeRepository
    {
        List<Grade> GetGrades();

        Grade GetGradeById(int id);

        List<Grade> GetGradesByCourseId(int courseId);

        List<Grade> GetGradesByStudentId(int studentId);

        List<Grade> GetGradesByStudentIdAndCourseId(int studentId, int courseId);

        Task<bool> AddGrade(Grade grade);


        Task<bool> UpdateGrade(Grade grade);
        void DeleteGrade(int id);
    }
}
