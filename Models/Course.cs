using System.Collections.Generic;

namespace CourseEvaluationSystem.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;

        public ICollection<Evaluation>? Evaluations { get; set; }
    }
}