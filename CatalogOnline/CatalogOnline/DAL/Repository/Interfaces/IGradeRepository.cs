using CatalogOnline.DAL.DBO;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IGradeRepository
    {
        List<Grade> GetGrades();
    }
}
