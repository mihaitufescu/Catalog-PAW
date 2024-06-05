namespace CatalogOnline.Models
{
    public class DisplayGradeModel
    {
        public int grade_id {  get; set; }
        public string course_name { get; set; }
        public int year { get; set; }
        public float score { get; set; }
        public string type_of_exam { get; set; }
        public float percentage { get; set; }
    }
}
