using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class NotificationsPageModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public List<NotificationModel> Notifications { get; set; }

        public NotificationsPageModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void OnGet()
        {
            Notifications = _notificationService.GetAllNotifications();
        }
    }
}
