using MigraDocCore.DocumentObjectModel.Tables;

namespace CatalogOnline.Models
{
    public class StudentCourseModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int Semester { get; set; }
        public bool Mandatory { get; set; }
        public int Year { get; set; }
        public string Group { get; set; }

        public double Medie { get; private set; } 

        public List<GradeDetailModel> Grades { get; set; } = new List<GradeDetailModel>();


        //De lene sunt aici puse ca nu mai stau sa fac repouri
        public double CalculateWeightedScore()
        {
            double weightedScore = 0;
            foreach (var grade in Grades)
            {
                double weight = grade.TypeOfExam == "Exam" ? 0.7 : 0.3;
                weightedScore += grade.Score * weight;
            }
            return weightedScore;
        }


        public void CalculateMedie()
        {
            double examScore = 0;
            double colocviuScore = 0;
            int examCount = 0;
            int colocviuCount = 0;

            foreach (var grade in Grades)
            {
                if (grade.TypeOfExam == "Exam")
                {
                    examScore += grade.Score;
                    examCount++;
                }
                else if (grade.TypeOfExam == "Colocviu")
                {
                    colocviuScore += grade.Score;
                    colocviuCount++;
                }
            }

            examScore = (examCount > 0) ? examScore / examCount : 0;
            colocviuScore = (colocviuCount > 0) ? colocviuScore / colocviuCount : 0;

            Medie = ((examScore * 0.7) + (colocviuScore * 0.3)) / 10;
        }

    }
}
