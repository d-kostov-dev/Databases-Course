namespace StudentsSystem.Client
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using StudentsSystem.Data;
    using StudentsSystem.Data.Migrations;

    public class EntryPoint
    {
        public static void Main()
        {
            Database.SetInitializer
                (new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());

            var dbConnection = new StudentSystemContext();

            using (dbConnection)
            {
                // Add course to Pesho.
                var studentPesho = dbConnection.Students.Find(1);
                studentPesho.Courses.Add(dbConnection.Courses.Find(3));
                studentPesho.Courses.Add(dbConnection.Courses.Find(1));
                dbConnection.SaveChanges();

                // Get and list all students.
                var result = dbConnection.Students
                    .Select(st => new { Student = st, Courses = st.Courses});

                foreach (var item in result)
                {
                    Console.WriteLine(item.Student.ID);
                    Console.WriteLine(item.Student.FirstName + " " + item.Student.LastName);
                    Console.WriteLine(item.Student.Number);
                    Console.WriteLine("\t{0}", string.Join(", ", item.Courses.Select(c => c.Name)));
                }
            }
        }
    }
}
