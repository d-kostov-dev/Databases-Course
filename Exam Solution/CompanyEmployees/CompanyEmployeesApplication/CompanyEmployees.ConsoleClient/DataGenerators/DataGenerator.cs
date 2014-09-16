namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using CompanyEmployees.ConsoleClient.Interfaces;

    public abstract class DataGenerator : IDataGenerator
    {
        private readonly IRandomValueGenerator random;
        private readonly ILogger<string> logger;
        private readonly DatabaseContext databaseConnection;
        private int itemsCount;

        public DataGenerator(
            DatabaseContext db, 
            IRandomValueGenerator radnomGenerator, 
            ILogger<string> logger, 
            int itemsToCreateCount)
        {
            this.random = radnomGenerator;
            this.itemsCount = itemsToCreateCount;
            this.databaseConnection = db;
            this.logger = logger;
        }

        public IRandomValueGenerator Random
        {
            get
            {
                return this.random;
            }
        }

        public DatabaseContext DatabaseConnection
        {
            get
            {
                return this.databaseConnection;
            }
        }

        public int ItemsCount
        {
            get
            {
                return this.itemsCount;
            }
        }

        public ILogger<string> Logger
        {
            get
            {
                return this.logger;
            }
        }

        public abstract void GenerateData();
    }
}
