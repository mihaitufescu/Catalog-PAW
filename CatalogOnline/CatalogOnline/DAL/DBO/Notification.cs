using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogOnline.DAL.DBO
{
    public class Notification
    {

        public Notification() { }

        [Key]
        public int notification_id { get; set; }


        [ForeignKey("User")]
        public int user_id { get; set; }

        [Required]
        public string message { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime date { get; set; }

    }



}
