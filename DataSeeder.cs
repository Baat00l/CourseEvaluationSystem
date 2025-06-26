using CourseEvaluationSystem.Models;

namespace CourseEvaluationSystem
{
    public static class DataSeeder
    {
        public static void SeedAll(AppDbContext context)
        {
            // Användare
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "admin", Password = "admin123", Role = "Admin" },
                    new User { Username = "student", Password = "student123", Role = "Student" }
                );
            }

            // Student
            if (!context.Students.Any())
            {
                context.Students.Add(new Student
                {
                    Name = "Test Student",
                    Email = "student@example.com"
                });
            }

            // Kurs
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course
                {
                    Title = "Introduktion till C#"
                });
            }

            context.SaveChanges();
        }
    }
}
