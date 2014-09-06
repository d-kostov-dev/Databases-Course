namespace ToysStore.ConsoleClient
{
    using System;

    using ToysStore.ConsoleClient.RandomGenerators;
    using ToysStore.ConsoleClient.Utilities;

    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Did you set your connection string? Y/N");

            string answer = Console.ReadLine();

            if (answer.ToLower() == "y")
            {
                Console.WriteLine("Great lets continue inserting items.");
            }
            else
            {
                Console.WriteLine("WTF are you expecting? FIX YOUR CONNECTION STRING!");
                return;
            }

            var databaseConnection = new DatabaseContext();
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
