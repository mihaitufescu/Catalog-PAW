using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.DAL.DBO
{
    public class Enrollment
    {
        [Key]
        public int enrollment_id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }
        public User User { get; set; }

        [ForeignKey("Course")]
        public int course_id { get; set; }
        public Course Course { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int semester { get; set; }
    }
}
