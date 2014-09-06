namespace BooksStore.ConsoleClient
{
    using System;
    using System.Linq;

    using BooksStore.ConsoleClient.Parsers;
    using BooksStore.Data;

    public class EntryPoint
    {
        private static ContextFactory contextFactory;

        public static void Main()
        {
            // Data Source=.\NEWSQL;
            contextFactory = new ContextFactory("DefaultDbConnectionString");

            // Data Source=.\SQLEXPRESS;
            // contextFactory = new ContextFactory("ExpressDbConnectionString"); 

            // Data Source=.;
            // contextFactory = new ContextFactory("GenericDbConnectionString"); 

            string fileToParse = "../../../XmlFiles/complex-books.xml";

            // var booksParser = new BooksParser(contextFactory, fileToParse).ParseFile();
        }
    }
}
