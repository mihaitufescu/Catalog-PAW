using AutoMapper;
using CatalogOnline.DAL;
using CatalogOnline.DAL.Repository;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;

namespace CatalogOnline.Services
{
    public class GradeService : IGradeService
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;  
        }

        public List<GradeModel> GetAllGrades()
        {
            var grades = _gradeRepository.GetGrades();
            return _mapper.Map<List<GradeModel>>(grades);
        }
    }
}
