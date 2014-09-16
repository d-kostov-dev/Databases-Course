namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using System;
    using System.Linq;

    using CompanyEmployees.ConsoleClient.Interfaces;
    using CompnayEmployees.Data;

    public class ReportsGenerator : DataGenerator
    {
        public ReportsGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var employeeIds = this.DatabaseConnection.Employees.Select(e => e.Id).ToList();

            for (int i = 0; i < employeeIds.Count; i++)
            {
                int numberOfReports = this.Random.GetRandomInt(25, 75);

                for (int j = 0; j < numberOfReports; j++)
                {
                    var currentReport = new Report()
                    {
                        EmployeeId = employeeIds[i],
                        ReportDate = this.Random.GetRandomDate(2000),
                    };

                    this.DatabaseConnection.Reports.Add(currentReport);
                }

                this.DatabaseConnection.SaveChanges();
                this.Logger.Log(".");
            }

            this.Logger.Log("\nReports Generated\n");
        }
    }
}
