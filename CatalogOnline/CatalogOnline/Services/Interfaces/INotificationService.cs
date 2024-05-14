using CatalogOnline.Models;

namespace CatalogOnline.Services.Interfaces
{
    public interface INotificationService
    {
        List<NotificationModel> GetAllNotifications();
        List<NotificationModel> GetCoursesByMessage(string message);
    }
}
