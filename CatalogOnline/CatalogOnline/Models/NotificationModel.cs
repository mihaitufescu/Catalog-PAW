namespace CatalogOnline.Models
{
    public class NotificationModel
    {

        public int notification_id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }

    }
}
