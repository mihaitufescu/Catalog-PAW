using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.DAL.DBO
{
    public class Document
    {

        public Document() { }

        [Key]
        public int document_id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string description { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string type { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string location { get; set; }
    }
}
