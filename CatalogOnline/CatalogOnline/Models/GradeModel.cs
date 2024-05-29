using CatalogOnline.DAL.DBO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class GradeModel
    {
        public int grade_id { get; set; }
        public float score { get; set; }
        public string type_of_exam { get; set; }
        public float percentage { get; set; }
    }
}
