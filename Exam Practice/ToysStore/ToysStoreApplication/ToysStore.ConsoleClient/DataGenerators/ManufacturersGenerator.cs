namespace ToysStore.ConsoleClient.RandomGenerators
{
    using System;

    using ToysStore.ConsoleClient.Interfaces;
    using ToysStore.Data;

    public class ManufacturersGenerator : DataGenerator
    {
        public ManufacturersGenerator(ToysStoreEntities databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var generatedNames = this.Random.GetUniqueRandomStringsSet(this.ItemsCount);

            int addedCounter = 0;

            foreach (var name in generatedNames)
            {
                this.DatabaseConnection.Manufacturers.Add(this.CreateItem(name));
                addedCounter++;

                if (addedCounter % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nManufacurers Generated\n");
        }

        private Manufacturer CreateItem(string manufacturerName)
        {
            return new Manufacturer()
            {
                Name = manufacturerName,
                Country = this.Random.GetRandomString(3),
            };
        }
    }
}
