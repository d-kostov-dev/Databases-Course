namespace ToysStore.ConsoleClient
{
    using ToysStore.ConsoleClient.RandomGenerators;
    using ToysStore.ConsoleClient.Utilities;
    using ToysStore.Data;

    public class EntryPoint
    {
        public static void Main()
        {
            var databaseConnection = new ToysStoreEntities();
            var randomGenerator = ValuesGenerator.GetInstance;
            var consoleLogger = new ConsoleLogger();
            var generatorsFactory = new GeneratorsFactory(databaseConnection, randomGenerator, consoleLogger);

            databaseConnection.Configuration.AutoDetectChangesEnabled = false;

            foreach (var generator in generatorsFactory.GetGenerators())
            {
                generator.GenerateData();
                databaseConnection.SaveChanges();
            }

            databaseConnection.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
