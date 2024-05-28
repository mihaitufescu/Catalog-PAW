using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;

namespace CatalogOnline.Services.Interfaces
{
    public interface IUserService
    {
        UserModel GetUserByName(string name);
        List<UserModel> GetUsers();
        User GetUserById(int id); 
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        void DeleteUser(int id);  
    }
}
