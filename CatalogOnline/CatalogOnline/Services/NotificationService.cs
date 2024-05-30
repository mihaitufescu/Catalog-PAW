using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        public List<NotificationModel> GetAllNotificationModels()
        {
            var notifications = _mapper.Map<List<NotificationModel>>(_notificationRepository.GetNotifications());
            return notifications;
        }

    }
}
