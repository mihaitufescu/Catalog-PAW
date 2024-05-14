using AutoMapper;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;

namespace CatalogOnline.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public List<NotificationModel> GetAllNotifications()
        {
            var courses = _notificationRepository.GetNotifications();
            return _mapper.Map<List<NotificationModel>>(courses);
        }


        List<NotificationModel> INotificationService.GetCoursesByMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
