namespace CompanyEmployees.ConsoleClient.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to generate random values of different types.
    /// </summary>
    public interface IRandomValueGenerator
    {
        int GetRandomInt(int minValue, int maxValue);

        double GetRandomDouble(double minValue, double maxValue);

        string GetRandomString(int length);

        string GetRandomLengthString();

        ISet<string> GetUniqueRandomStringsSet(int listLength);

        ISet<int> GetUniqueRandomIntegersSet(int listLength, int minValue, int maxValue);

        DateTime GetRandomDate(int minimalYear);
    }
}
