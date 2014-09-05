namespace ToysStore.ConsoleClient.Interfaces
{
    using System.Collections.Generic;

    public interface IGeneratorsFactory
    {
        IList<IDataGenerator> GetGenerators();
    }
}
