using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Services.Interfaces
{
    public interface INotificationService
    {        
        List<NotificationModel> GetAllNotificationModels();

    }
}
