namespace CompanyEmployees.ConsoleClient
{
    using System;

    using CompanyEmployees.ConsoleClient.RandomGenerators;
    using CompanyEmployees.ConsoleClient.Utilities;

    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Did you set your connection string? Y/N");

            string answer = Console.ReadLine();

            if (answer.ToLower() == "y")
            {
                Console.WriteLine("Great lets continue inserting data.");
            }
            else
            {
                Console.WriteLine("Please fix your connection string and try again!");
                return;
            }

            var databaseConnection = new DatabaseContext(); // If your create your own context/entities class change the inheritance here
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
