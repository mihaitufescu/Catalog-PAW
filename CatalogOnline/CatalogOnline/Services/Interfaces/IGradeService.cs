using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Services.Interfaces
{
    public interface IGradeService
    {
        Grade GetGradeById(int gradeId);

        List<GradeModel> GetAllGrades();

        GradeModel GetGradeModelById(int gradeId);

        Task<bool> AddGrade(Grade grade);

        Task<bool> UpdateGrade(Grade grade);

        void DeleteGrade(int gradeId);
    }
}
