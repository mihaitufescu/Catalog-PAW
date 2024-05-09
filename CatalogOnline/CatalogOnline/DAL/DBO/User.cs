using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CatalogOnline.DAL.DBO
{
    public class User
    {
        public User()
        {
            //CourseAttendance = new List<CourseAttendance>();
            //Notifications = new List<Notifications>();
            //Grades = new List<Grades>();
        }
        [Key]
        public int user_id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string legal_name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string password { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string role { get; set; }
    }
}
