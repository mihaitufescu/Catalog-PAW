using AutoMapper;
using CatalogOnline.DAL.DBO;
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

        public EnrollmentService(IMapper mapper, IEnrollmentRepository enrollmentRepository)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
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

        public bool AddEnrollmentModel(EnrollmentModel enrollment)
        {
            var enrollmentEntity = _mapper.Map<Enrollment>(enrollment);
            return _enrollmentRepository.AddEnrollment(enrollmentEntity).Result;
        }

        public bool AddEnrollment(Enrollment enrollment)
        {
            return _enrollmentRepository.AddEnrollment(enrollment).Result;
        }

        public bool UpdateEnrollment(Enrollment enrollment)
        {
            return _enrollmentRepository.UpdateEnrollment(enrollment).Result;
        }

        public void DeleteEnrollment(int id)
        {
            _enrollmentRepository.DeleteEnrollment(id);
        }
    }
}
