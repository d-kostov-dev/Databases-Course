namespace StudentsSystem.Data
{
    using System.Data.Entity;
    using StudentsSystem.Model;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("StudentSystemDb")
        { 
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
