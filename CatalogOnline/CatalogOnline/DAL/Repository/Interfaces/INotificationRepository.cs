using CatalogOnline.DAL.DBO;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface INotificationRepository
    {
        public List<Notification> GetNotifications(); 
    }
}
