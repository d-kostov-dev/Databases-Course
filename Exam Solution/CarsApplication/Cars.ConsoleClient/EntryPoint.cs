namespace Cars.ConsoleClient
{
    using System;
    using System.Linq;

    using Cars.Data;

    public class EntryPoint
    {
        private static CarsContext databaseConnection;

        public static void Main()
        {
            Console.WriteLine("For the first start...and testing...please uncomment the parser. Thank you!");

            databaseConnection = new CarsContext("GenericDbConnectionString");
            databaseConnection.Configuration.AutoDetectChangesEnabled = false;

            string pathToFiles = "../../../DataFiles/";
            string fileNameFormat = "data.{0}.json";
            string queriesFile = "../../../Queries/Queries.xml";

            //Console.WriteLine("Parsing Data");
            //var parser = new JsonDataParser(databaseConnection, pathToFiles, fileNameFormat);
            //parser.ParseData();

            Console.WriteLine("Executing Queries");
            var queryExecutor = new XmlQueryExecutor(databaseConnection, queriesFile);
            queryExecutor.ExecuteQueries();

            databaseConnection.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
