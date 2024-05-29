using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class AddNotificationModel : PageModel
    {
        private readonly ILogger<AddNotificationModel> _logger;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public AddNotificationModel(INotificationService notificationService, IUserService userService, ILogger<AddNotificationModel> logger)
        {
            _notificationService = notificationService;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public required NotificationRegisterModel Notification { get; set; }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = _userService.GetAllUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValidNotification(Notification))
            {
                Users = _userService.GetAllUsers();
                return Page();
            }

            var newNotification = new Notification
            {
                user_id = Notification.user_id,
                message = Notification.message,
                date = Notification.date
            };

            var res = await _notificationService.AddNotification(newNotification);
            if (!res)
            {
                ModelState.AddModelError("AddNotificationError", "Failed to add notification");
                Users = _userService.GetAllUsers();
                return Page();
            }

            return RedirectToPage("Notification");
        }

        private bool IsValidNotification(NotificationRegisterModel notification)
        {
            bool isValid = true;

            if (notification.user_id <= 0)
            {
                ModelState.AddModelError("Notification.user_id", "User ID must be greater than 0.");
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
