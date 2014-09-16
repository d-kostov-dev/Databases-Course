namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CompanyEmployees.ConsoleClient.Interfaces;
    using CompnayEmployees.Data;

    public class EmployeesToProjectGenerator : DataGenerator
    {
        public EmployeesToProjectGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var employeeIds = this.DatabaseConnection.Employees.Select(e => e.Id).ToList();
            var projectsIds = this.DatabaseConnection.Projects.Select(p => p.Id).ToList();

            var addedCounter = 0;
            foreach (var project in projectsIds)
            {
                int empWorkingOnTheProject = this.Random.GetRandomInt(2, 20);
                var uniqueEmployees = new HashSet<int>();

                while (uniqueEmployees.Count < empWorkingOnTheProject)
                {
                    uniqueEmployees.Add(employeeIds[this.Random.GetRandomInt(0, employeeIds.Count - 1)]);
                }

                foreach (var item in uniqueEmployees)
                {
                    var currentEmpProject = new EmployeeProject();

                    currentEmpProject.EmployeeId = item;
                    currentEmpProject.ProjectId = project;

                    var startDate = this.Random.GetRandomDate(2000);
                    var endDate = this.Random.GetRandomDate(2010);

                    while (startDate > endDate)
                    {
                        endDate = this.Random.GetRandomDate(2010);
                    }

                    currentEmpProject.StartDate = startDate;
                    currentEmpProject.EndDate = endDate;

                    this.DatabaseConnection.EmployeeProjects.Add(currentEmpProject);

                    addedCounter++;
                    if (addedCounter % 100 == 0)
                    {
                        this.DatabaseConnection.SaveChanges();
                        this.Logger.Log(".");
                    }
                }
            }

            this.Logger.Log("\nEmployee To Projects Generated\n");
        }
    }
}
