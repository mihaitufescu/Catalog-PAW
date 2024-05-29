using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> AddNotification(Notification notification);
        
        List<NotificationModel> GetAllNotificationModels();
        List<Notification> GetAllNotifications();
        Notification GetNotificationById(int id);
        Task<bool> UpdateNotification(Notification notification);
        void DeleteNotification(int id);
    }
}
