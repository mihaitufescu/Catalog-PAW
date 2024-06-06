using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "student")]
    public class ViewNotificationsModel : PageModel
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IUserRepository userRepository;

        public ViewNotificationsModel(INotificationRepository _notificationRepository, IUserRepository _userRepository)
        {
            notificationRepository = _notificationRepository;
            userRepository = _userRepository;
        }

        public List<Notification> Notifications { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                Notifications = notificationRepository.GetNotificationsByUserId(int.Parse(userId)).ToList();
            }
        }
    }
}

