namespace Cars.Data
{
    using System.Data.Entity;

    using Cars.Model;
    using Cars.Data.Migrations;
    
    public class CarsContext : DbContext
    {
        public CarsContext()
            :this("GenericDbConnectionString")
        {
        }

        public CarsContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarsContext, Configuration>());
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Dealer> Dealers { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
    }
}
