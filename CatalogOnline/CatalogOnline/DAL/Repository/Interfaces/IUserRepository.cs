using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;

namespace CatalogOnline.DAL.Repository.Interfaces

{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserByName(string name);
    }
}
