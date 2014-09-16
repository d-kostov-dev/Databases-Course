namespace CompanyEmployees.ConsoleClient.Utilities
{
    using System.Collections.Generic;

    using CompanyEmployees.ConsoleClient.Interfaces;
    using CompanyEmployees.ConsoleClient.RandomGenerators;

    public class GeneratorsFactory : IGeneratorsFactory
    {
        private readonly DatabaseContext databaseConnection;
        private readonly IRandomValueGenerator randomGenerator;
        private readonly ILogger<string> logger;

        public GeneratorsFactory(
            DatabaseContext databaseConnection, 
            IRandomValueGenerator randomGenerator, 
            ILogger<string> logger)
        {
            this.databaseConnection = databaseConnection;
            this.randomGenerator = randomGenerator;
            this.logger = logger;
        }

        public IList<IDataGenerator> GetGenerators()
        {
            var generatorsList = new List<IDataGenerator>();

            generatorsList.Add(new DepartmentsGenerator(this.databaseConnection, this.randomGenerator, this.logger, 100));
            generatorsList.Add(new ProjectsGenerator(this.databaseConnection, this.randomGenerator, this.logger, 1000));
            generatorsList.Add(new EmployeesGenerator(this.databaseConnection, this.randomGenerator, this.logger, 5000));
            generatorsList.Add(new EmployeesToProjectGenerator(this.databaseConnection, this.randomGenerator, this.logger, 0)); // Unknown number of data
            generatorsList.Add(new ReportsGenerator(this.databaseConnection, this.randomGenerator, this.logger, 0)); // Unknown number of data

            return generatorsList;
        }
    }
}
