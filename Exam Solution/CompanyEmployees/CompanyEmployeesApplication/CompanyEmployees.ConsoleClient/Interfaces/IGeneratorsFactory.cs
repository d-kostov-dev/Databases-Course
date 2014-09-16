namespace CompanyEmployees.ConsoleClient.Interfaces
{
    using System.Collections.Generic;

    public interface IGeneratorsFactory
    {
        IList<IDataGenerator> GetGenerators();
    }
}
