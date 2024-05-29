using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class DocumentRegisterModel
    {
        [Required(ErrorMessage = "Document ID is required")]
        public int document_id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int user_id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public string location { get; set; }
    }
}
