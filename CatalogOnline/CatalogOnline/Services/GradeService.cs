using AutoMapper;
using CatalogOnline.DAL;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> AddGrade(Grade grade)
        {
            return await _gradeRepository.AddGrade(grade);
        }

        public async Task<bool> UpdateGrade(Grade grade)
        {
            await _gradeRepository.UpdateGrade(grade);
            return true;
            
        }

        public void DeleteGrade(int id)
        {
              _gradeRepository.DeleteGrade(id);
        }

        Grade IGradeService.GetGradeById(int gradeId)
        {
            var grade = _gradeRepository.GetGradeById(gradeId);
            return _mapper.Map<Grade>(grade);
        }
    }
}
