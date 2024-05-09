using AutoMapper;
using CatalogOnline.DAL.Repository;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;

namespace CatalogOnline.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public UserModel GetUserByName(string name)
        {
            var user = _mapper.Map<UserModel>(_userRepository.GetUserByName(name));

            return user;
        }

        public List<UserModel> GetUsers()
        {
            var users = _mapper.Map<List<UserModel>>(_userRepository.GetUsers());

            return users;
        }
    }
}
