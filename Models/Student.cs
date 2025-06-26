using System.Collections.Generic;

namespace CourseEvaluationSystem.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Evaluation>? Evaluations { get; set; }
    }
}