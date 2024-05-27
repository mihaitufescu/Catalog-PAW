using CatalogOnline.DAL.DBO;
using System.Collections.Generic;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByName(string name);
        List<User> GetUsers();
        User GetUserById(int id);  
        void AddUser(User user);  
        void UpdateUser(User user);  
        void DeleteUser(int id);  
    }
}
