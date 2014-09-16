namespace CompanyEmployees.ConsoleClient.Utilities
{
    using System;

    using CompanyEmployees.ConsoleClient.Interfaces;

    public class ConsoleLogger : ILogger<string>
    {
        public void Log(string data)
        {
            Console.Write(data);
        }
    }
}
