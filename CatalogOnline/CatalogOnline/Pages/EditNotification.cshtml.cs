using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class EditNotificationModel : PageModel
    {
        private readonly ILogger<EditNotificationModel> logger;
        private readonly INotificationRepository notificationRepository;
        private readonly IUserRepository userRepository;

        public EditNotificationModel(INotificationRepository _notificationRepository, IUserRepository _userRepository,
            ILogger<EditNotificationModel> _logger)
        {
            notificationRepository = _notificationRepository;
            userRepository = _userRepository;
            logger = _logger;
        }

        [BindProperty]
        public required NotificationRegisterModel Notification { get; set; }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var notification = notificationRepository.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound();
            }

            Notification = new NotificationRegisterModel
            {
                notification_id = notification.notification_id,
                user_id = notification.user_id,
                message = notification.message,
                date = notification.date
            };

            Users = userRepository.GetAllUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !await IsValidNotification(Notification))
            {
                Users = userRepository.GetAllUsers();
                return Page();
            }

            var updatedNotification = new Notification
            {
                notification_id = Notification.notification_id,
                user_id = Notification.user_id,
                message = Notification.message,
                date = Notification.date
            };

            var res =  notificationRepository.UpdateNotification(updatedNotification);
            if (res is null)
            {
                ModelState.AddModelError("EditNotificationError", "Failed to update notification");
                Users = userRepository.GetAllUsers();
                return Page();
            }

            return RedirectToPage("Notification");
        }

        private async Task<bool> IsValidNotification(NotificationRegisterModel notification)
        {
            bool isValid = true;

            if (notification.user_id <= 0 || !await userRepository.UserExists(notification.user_id))
            {
                ModelState.AddModelError("Notification.user_id", "User ID must be greater than 0 and must exist.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(notification.message))
            {
                ModelState.AddModelError("Notification.message", "Message is required.");
                isValid = false;
            }

            if (notification.date == DateTime.MinValue)
            {
                ModelState.AddModelError("Notification.created_at", "Created at date is required.");
                isValid = false;
            }

            return isValid;
        }
    }
}
