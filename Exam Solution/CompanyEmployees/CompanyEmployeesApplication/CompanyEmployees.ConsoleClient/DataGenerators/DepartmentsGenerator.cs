namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using System;

    using CompanyEmployees.ConsoleClient.Interfaces;
    using CompnayEmployees.Data;

    public class DepartmentsGenerator : DataGenerator
    {
        public DepartmentsGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var generatedNames = this.Random.GetUniqueRandomStringsSet(this.ItemsCount);

            int addedCounter = 0;

            foreach (var name in generatedNames)
            {
                this.DatabaseConnection.Departments.Add(this.CreateItem(name));

                addedCounter++;
                if (addedCounter % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nDepartments Generated\n");
        }

        private Department CreateItem(string name)
        {
            return new Department()
            {
                Name = name
            };
        }
    }
}
