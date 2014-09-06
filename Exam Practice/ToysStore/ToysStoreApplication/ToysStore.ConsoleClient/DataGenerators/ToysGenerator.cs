namespace ToysStore.ConsoleClient.RandomGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ToysStore.ConsoleClient.Interfaces;
    using ToysStore.Data;

    public class ToysGenerator : DataGenerator
    {
        public ToysGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var categoriesIds = this.DatabaseConnection.Categories.Select(c => c.ID).ToList();
            var ageRangesIds = this.DatabaseConnection.AgeRanges.Select(ar => ar.ID).ToList();
            var manufacturersIds = this.DatabaseConnection.Manufacturers.Select(m => m.ID).ToList();

            for (int i = 0; i < this.ItemsCount; i++)
            {
                var manufacturerId = manufacturersIds[this.Random.GetRandomInt(0, manufacturersIds.Count - 1)];
                var ageRangeId = ageRangesIds[this.Random.GetRandomInt(0, manufacturersIds.Count - 1)];

                var currentToy = this.CreateItem(manufacturerId, ageRangeId);
                this.AddSubItems(currentToy, categoriesIds);

                this.DatabaseConnection.Toys.Add(currentToy);

                if (i % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nToys Generated\n");
        }

        private void AddSubItems(Toy currentToy, IList<int> categoriesIds)
        {
            int subItemsCount = this.Random.GetRandomInt(0, 5);
            var uniqueSubItems = new HashSet<int>();

            while (uniqueSubItems.Count < subItemsCount)
            {
                uniqueSubItems.Add(categoriesIds[this.Random.GetRandomInt(0, categoriesIds.Count - 1)]);
            }

            foreach (var item in uniqueSubItems)
            {
                currentToy.Categories.Add(this.DatabaseConnection.Categories.Find(item));
            }
        }

        private Toy CreateItem(int manufacturer, int ageRange)
        {
            return new Toy()
            {
                Name = this.Random.GetRandomLengthString(),
                Type = this.Random.GetRandomString(5),
                Color = this.Random.GetRandomString(3),
                Price = (decimal)this.Random.GetRandomDouble(1.0, 49.99),
                AgeRangeId = ageRange,
                ManufacturerId = manufacturer,
            };
        }
    }
}
