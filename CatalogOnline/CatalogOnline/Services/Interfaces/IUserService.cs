using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;

namespace CatalogOnline.Services.Interfaces
{
    public interface IUserService
    {
        UserModel GetUserByName(string name);
        List<UserModel> GetUserModels();

    }
}
