using CatalogOnline.DAL.DBO;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface INotificationRepository
    {
        public List<Notification> GetNotifications(); 
        
        public Notification GetNotificationById(int id);
        Task<bool> AddNotification(Notification notification);
        Task<bool> UpdateNotification(Notification notification);
        void DeleteNotification(int id);

    }

}
