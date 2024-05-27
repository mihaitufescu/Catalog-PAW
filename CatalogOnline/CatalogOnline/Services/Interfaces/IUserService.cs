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
        void AddUser(User user);  
        void UpdateUser(User user);
        void DeleteUser(int id);  
    }
}
