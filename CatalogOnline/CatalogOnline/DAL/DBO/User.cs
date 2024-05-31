using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.DAL.DBO
{
    public class User
    {
        public User()
        {
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

        [Column(TypeName = "int")]
        public int? year_of_study { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string? group { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
