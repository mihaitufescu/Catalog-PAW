using AutoMapper;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System;
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
    }
}
