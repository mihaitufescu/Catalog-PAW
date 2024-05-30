namespace CatalogOnline.Models
{
    public class StudentCourseGradeModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public DateTime DateJoined { get; set; }
        public List<GradeModel> Grades { get; set; }
        public float AverageScore { get; set; }
    }
}
