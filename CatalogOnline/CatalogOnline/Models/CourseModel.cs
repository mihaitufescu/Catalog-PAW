namespace CatalogOnline.Models
{
    public class CourseModel
    {
        public int course_id { get; set; }
        public string subject { get; set; }
        public bool mandatory { get; set; }
        public int credits_number { get; set; }
        public int year { get; set; }
    }
}
