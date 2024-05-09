using CatalogOnline.Models;

namespace CatalogOnline.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
        UserModel GetUserByName(string name);
    }
}