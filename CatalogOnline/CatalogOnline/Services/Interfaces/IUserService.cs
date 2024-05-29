using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;

namespace CatalogOnline.Services.Interfaces
{
    public interface IUserService
    {
        UserModel GetUserByName(string name);
        List<UserModel> GetUserModels();
        Task<bool> UserExists(int userId);

        List<User> GetAllUsers();
        User GetUserById(int id); 
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        void DeleteUser(int id);
        List<User> GetAllStudents();
    }
}
