﻿namespace ToysStore.ConsoleClient.RandomGenerators
{
    using System;

    using ToysStore.ConsoleClient.Interfaces;
    using ToysStore.Data;

    public class CategoriesGenerator : DataGenerator
    {
        public CategoriesGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            var generatedNames = this.Random.GetUniqueRandomStringsSet(this.ItemsCount);

            int addedCounter = 0;

            foreach (var name in generatedNames)
            {
                this.DatabaseConnection.Categories.Add(this.CreateItem(name));
                addedCounter++;

                if (addedCounter % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nCategories Generated\n");
        }

        private Category CreateItem(string categoryName)
        {
            return new Category()
            {
                Name = categoryName
            };
        }
    }
}
