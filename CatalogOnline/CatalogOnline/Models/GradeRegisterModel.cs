using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class GradeRegisterModel
    {

        [Required]
        public int user_id { get; set; }

        [Required]
        public int course_id { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Score must be greater than 0.")]
        public int score { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Percentage must be greater than 0.")]
        public int percentage { get; set; }

        [Required]
        [StringLength(50)]
        public string type_of_exam { get; set; }
    }
}
