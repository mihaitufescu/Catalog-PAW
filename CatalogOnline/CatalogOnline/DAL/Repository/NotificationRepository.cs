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

    }
}
