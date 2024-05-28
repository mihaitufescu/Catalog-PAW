using CatalogOnline.DAL.DBO;
using System.Collections.Generic;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByName(string name);
        List<User> GetUsers();
        User GetUserById(int id);  
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);  
        void DeleteUser(int id);  
    }
}
