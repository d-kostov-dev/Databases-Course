namespace ToysStore.ConsoleClient.RandomGenerators
{
    using System;

    using ToysStore.ConsoleClient.Interfaces;
    using ToysStore.Data;

    public class AgeRangesGenerator : DataGenerator
    {
        public AgeRangesGenerator(DatabaseContext databaseConnection, IRandomValueGenerator randomGenerator, ILogger<string> logger, int itemsCount)
            : base(databaseConnection, randomGenerator, logger, itemsCount)
        {
        }

        public override void GenerateData()
        {
            for (int i = 0; i < this.ItemsCount; i++)
            {
                int minAge = this.Random.GetRandomInt(0, 18);
                int maxAge = this.Random.GetRandomInt(minAge + 1, 105);

                this.DatabaseConnection.AgeRanges.Add(this.CreateItem(minAge, maxAge));

                if (i % 100 == 0)
                {
                    this.DatabaseConnection.SaveChanges();
                    this.Logger.Log(".");
                }
            }

            this.Logger.Log("\nAge Ranges Generated\n");
        }

        private AgeRanx CreateItem(int minAge, int maxAge)
        {
            return new AgeRanx()
            {
                MinAge = minAge,
                MaxAge = maxAge,
            };
        }
    }
}
