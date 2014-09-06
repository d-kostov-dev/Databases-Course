namespace ToysStore.ConsoleClient.Utilities
{
    using System.Collections.Generic;

    using ToysStore.ConsoleClient.Interfaces;
    using ToysStore.ConsoleClient.RandomGenerators;

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

            generatorsList.Add(new CategoriesGenerator(this.databaseConnection, this.randomGenerator, this.logger, 10000));
            generatorsList.Add(new ManufacturersGenerator(this.databaseConnection, this.randomGenerator, this.logger, 100));
            generatorsList.Add(new AgeRangesGenerator(this.databaseConnection, this.randomGenerator, this.logger, 100));
            generatorsList.Add(new ToysGenerator(this.databaseConnection, this.randomGenerator, this.logger, 20000));

            return generatorsList;
        }
    }
}
