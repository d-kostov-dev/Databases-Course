namespace StudentsSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StudentsSystem.Model;
    
    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystemContext context)
        {
            var cSharpOne = new Course()
            {
                ID = 1,
                Name = "C# 1",
            };

            var cSharpTwo = new Course()
            {
                ID = 2,
                Name = "C# 2",
            };

            var cSharpOOP = new Course()
            {
                ID = 3,
                Name = "C# OOP",
            };

            context.Courses.AddOrUpdate(cSharpOne);
            context.Courses.AddOrUpdate(cSharpTwo);
            context.Courses.AddOrUpdate(cSharpOOP);

            var studentOne = new Student()
            {
                ID = 1,
                FirstName = "Pesho",
                LastName = "Stamatov",
                Number = 123456,
            };

            var studentTwo = new Student()
            {
                ID = 2,
                FirstName = "Kencho",
                LastName = "Ivailov",
                Number = 567123,
            };

            context.Students.AddOrUpdate(studentOne);
            context.Students.AddOrUpdate(studentTwo);
        }
    }
}
