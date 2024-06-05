using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "admin")]
    public class AddNotificationModel : PageModel
    {
        private readonly ILogger<AddNotificationModel> logger;
        private readonly INotificationRepository notificationRepository;
        private readonly IUserRepository userRepository;

        public AddNotificationModel(INotificationRepository _notificationRepository, IUserRepository _userRepository,
            ILogger<AddNotificationModel> _logger)
        {
            notificationRepository = _notificationRepository;
            userRepository = _userRepository;
            logger = _logger;
        }

        [BindProperty]
        public required NotificationRegisterModel Notification { get; set; }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = userRepository.GetAllUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValidNotification(Notification))
            {
                Users = userRepository.GetAllUsers();
                return Page();
            }

            var newNotification = new Notification
            {
                user_id = Notification.user_id,
                message = Notification.message,
                date = Notification.date
            };

            var res =  notificationRepository.AddNotification(newNotification);
            if (res is null)
            {
                ModelState.AddModelError("AddNotificationError", "Failed to add notification");
                Users = userRepository.GetAllUsers();
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
