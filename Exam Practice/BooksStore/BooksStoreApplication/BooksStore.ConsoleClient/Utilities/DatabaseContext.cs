namespace BooksStore.ConsoleClient.Utilities
{
    using BooksStore.Data;

    public class DatabaseContext : BooksStoreContext
    {
        public DatabaseContext(string connectionString)
            :base(connectionString)
        {
        }
    }
}
