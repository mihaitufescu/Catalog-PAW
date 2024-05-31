using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class EnrollmentRegisterModel
    {
        [Required(ErrorMessage = "User ID is required")]
        public int enrollment_id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int user_id { get; set; }
        [Required(ErrorMessage = "Course ID is required")]
        public int course_id { get; set; }
        [Required(ErrorMessage = "Joined since is required")]
        public int semester { get; set; }
    }
}
