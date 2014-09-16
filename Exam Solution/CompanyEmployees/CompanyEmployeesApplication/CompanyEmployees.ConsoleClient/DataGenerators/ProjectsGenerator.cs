namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using System;

    using CompanyEmployees.ConsoleClient.Interfaces;
    using CompnayEmployees.Data;

    public class ProjectsGenerator : DataGenerator
    {
        public ProjectsGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var generatedNames = this.Random.GetUniqueRandomStringsSet(this.ItemsCount);

            int addedCounter = 0;

            foreach (var name in generatedNames)
            {
                this.DatabaseConnection.Projects.Add(this.CreateItem(name));

                addedCounter++;
                if (addedCounter % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nProjects Generated\n");
        }

        private Project CreateItem(string name)
        {
            return new Project()
            {
                Name = name
            };
        }
    }
}
