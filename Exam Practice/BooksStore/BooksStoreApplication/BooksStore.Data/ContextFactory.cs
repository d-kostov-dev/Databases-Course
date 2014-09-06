//namespace BooksStore.Data
//{
//    using System.Data.Entity.Infrastructure;

//    public class ContextFactory : IDbContextFactory<BooksStoreContext>
//    {
//        private const string DEFAULT_CONNECTION_STRING = "DefaultDbConnectionString";
//        private string connectionString;

//        public ContextFactory()
//            : this(null)
//        {
//        }

//        public ContextFactory(string userConnectionString)
//        {
//            if (userConnectionString == null)
//            {
//                userConnectionString = DEFAULT_CONNECTION_STRING;
//            }

//            this.connectionString = userConnectionString;
//        }

//        public BooksStoreContext Create()
//        {
//            return new BooksStoreContext(connectionString);
//        }
//    }
//}
