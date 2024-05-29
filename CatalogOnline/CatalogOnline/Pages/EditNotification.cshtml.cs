using CatalogOnline.DAL.DBO;
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
        private readonly ILogger<EditNotificationModel> _logger;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public EditNotificationModel(INotificationService notificationService, IUserService userService, ILogger<EditNotificationModel> logger)
        {
            _notificationService = notificationService;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public required NotificationRegisterModel Notification { get; set; }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var notification = _notificationService.GetNotificationById(id);
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

            Users = _userService.GetAllUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !await IsValidNotification(Notification))
            {
                Users = _userService.GetAllUsers();
                return Page();
            }

            var updatedNotification = new Notification
            {
                notification_id = Notification.notification_id,
                user_id = Notification.user_id,
                message = Notification.message,
                date = Notification.date
            };

            var res = await _notificationService.UpdateNotification(updatedNotification);
            if (!res)
            {
                ModelState.AddModelError("EditNotificationError", "Failed to update notification");
                Users = _userService.GetAllUsers();
                return Page();
            }

            return RedirectToPage("Notification");
        }

        private async Task<bool> IsValidNotification(NotificationRegisterModel notification)
        {
            bool isValid = true;

            if (notification.user_id <= 0 || !await _userService.UserExists(notification.user_id))
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
