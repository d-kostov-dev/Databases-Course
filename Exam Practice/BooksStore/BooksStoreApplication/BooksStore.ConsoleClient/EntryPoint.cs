namespace BooksStore.ConsoleClient
{
    using System;
    using System.Linq;

    using BooksStore.ConsoleClient.Parsers;
    using BooksStore.Data;
    using BooksStore.ConsoleClient.Utilities;

    public class EntryPoint
    {
        private static DatabaseContext databaseConnection;

        public static void Main()
        {
            // Data Source=.\NEWSQL;
            databaseConnection = new DatabaseContext("DefaultDbConnectionString");

            // Data Source=.\SQLEXPRESS;
            //databaseConnection = new DatabaseContext("ExpressDbConnectionString");

            // Data Source=.;
            //databaseConnection = new DatabaseContext("GenericDbConnectionString");

            string fileToParse = "../../../XmlFiles/complex-books.xml";
            string queriesFile = "../../../XmlFiles/reviews-queries.xml";
            string queriesResultsFile = "../../../XmlFiles/reviews-search-results.xml";

            //new BooksParser(databaseConnection, fileToParse).ParseFile();
            new XmlQueryExecutor(databaseConnection, queriesFile, queriesResultsFile).ExecuteQueries();
        }
    }
}
