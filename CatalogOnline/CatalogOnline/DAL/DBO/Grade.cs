using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.DAL.DBO
{
    public class Grade
    {
        [Key]
        public int grade_id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }

        [ForeignKey("Course")]
        public int course_id { get; set; }

        [Column(TypeName = "float")]
        public float score { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string type_of_exam { get; set; }

        [Column(TypeName = "float")]
        public float percentage { get; set; }
        
    }

}
