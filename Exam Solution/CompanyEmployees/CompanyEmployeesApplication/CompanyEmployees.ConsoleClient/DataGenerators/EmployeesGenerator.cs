namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CompanyEmployees.ConsoleClient.Interfaces;
    using CompnayEmployees.Data;
    
    public class EmployeesGenerator : DataGenerator
    {
        public EmployeesGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var departmentIds = this.DatabaseConnection.Departments.Select(d => d.Id).ToList();
            var projectsIds = this.DatabaseConnection.Projects.Select(p => p.Id).ToList();

            for (int i = 0; i < this.ItemsCount; i++)
            {
                var currentEmployee = this.CreateItem();

                var empDepartment = this.DatabaseConnection.Departments.Find(departmentIds[this.Random.GetRandomInt(0, departmentIds.Count - 1)]);
                currentEmployee.Department = empDepartment;

                if (!(i % 95 == 0))
                {
                    // Add Manager
                    var managerIds = 
                        this.DatabaseConnection.Employees
                        .Where(e => e.ManagerId == null)
                        .Select(e => e.Id)
                        .ToList();

                    currentEmployee.ManagerId = managerIds[this.Random.GetRandomInt(0, managerIds.Count - 1)];
                }

                this.DatabaseConnection.Employees.Add(currentEmployee);

                if (i % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nEmployees Generated\n");
        }

        private Employee CreateItem()
        {
            return new Employee()
            {
                FirstName = this.Random.GetRandomLengthString(),
                LastName = this.Random.GetRandomLengthString(),
                Salary = (decimal)this.Random.GetRandomDouble(50000.0, 200000.0),
            };
        }
    }
}
