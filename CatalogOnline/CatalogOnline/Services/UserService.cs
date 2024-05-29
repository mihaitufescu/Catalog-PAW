using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CatalogOnline.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IDocumentRepository documentRepository;

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

        public List<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public List<UserModel> GetUserModels()
        {
            var users = _mapper.Map<List<UserModel>>(_userRepository.GetUsers());
            return users;
        }

        public List<User> GetAllStudents(){
            return _userRepository.GetAllStudent();

        }
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public async Task<bool> AddUser(User user)
        {
            var res = await _userRepository.AddUser(user);
            return res;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var res = await _userRepository.UpdateUser(user);
            return res;
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }


        public async Task<bool> UserExists(int userId)
        {
            return await _userRepository.UserExists(userId);
        }

    }
}
