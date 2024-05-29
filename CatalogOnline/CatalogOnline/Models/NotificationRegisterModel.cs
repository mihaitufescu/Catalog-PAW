using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class NotificationRegisterModel
    {
        [Required (ErrorMessage = "User ID is required")]
        public int notification_id { get; set; }
        [Required (ErrorMessage = "User ID is required")]
        public int user_id { get; set; }
        [Required (ErrorMessage = "Message is required")]
        public string message { get; set; }
        [Required (ErrorMessage = "Date is required")]
        public DateTime date { get; set; }
    }
}
