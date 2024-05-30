using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class NotificationsPageModel : PageModel
    {
        private readonly INotificationService notificationService;

        private readonly INotificationRepository notificationRepository;

        public List<NotificationModel> Notifications { get; set; }

        public NotificationsPageModel(INotificationService _notificationService, INotificationRepository notificationRepository)
        {
            notificationService = _notificationService;
            this.notificationRepository = notificationRepository;
        }

        public void OnGet()
        {
            Notifications = notificationService.GetAllNotificationModels();
        }

        
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            notificationRepository.DeleteNotification(id);
            return RedirectToPage();
        }
    }
}
