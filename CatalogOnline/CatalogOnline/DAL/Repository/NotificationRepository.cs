using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.DAL.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext dataContext) {
            _context = dataContext;
         }

        public List<Notification> GetNotifications() {
            return _context.Notification.OrderBy(p => p.notification_id).ToList();

        }

        public Notification GetNotificationById(int id)
        {
            return _context.Notification.Find(id);
        }

        public async Task<bool> AddNotification(Notification notification)
        {
            await _context.Notification.AddAsync(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateNotification(Notification notification)
        {
            _context.Notification.Update(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public void DeleteNotification(int id)
        {
          
            var notification = _context.Notification.Find(id);
            if (notification != null)
            {
                _context.Notification.Remove(notification);
                _context.SaveChanges();
            }
        }

        public List<Notification> GetNotificationsByUserId(int userId)
        {
            return _context.Notification.Where(n => n.user_id == userId).ToList();
        }

    }
}
