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

        [ForeignKey("Course")]
        public int course_id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime joined_since { get; set; }
    }
}
