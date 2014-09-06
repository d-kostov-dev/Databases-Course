namespace BooksStore.Data
{
    using System.Data.Entity;

    using BooksStore.Model;
    using BooksStore.Data.Migrations;

    public class BooksStoreContext : DbContext
    {
        public BooksStoreContext()
            :this("DefaultDbConnectionString")
        {
        }
        public BooksStoreContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BooksStoreContext, Configuration>());
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
