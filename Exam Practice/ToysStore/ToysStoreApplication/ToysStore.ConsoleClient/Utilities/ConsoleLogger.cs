namespace ToysStore.ConsoleClient.Utilities
{
    using System;

    using ToysStore.ConsoleClient.Interfaces;

    public class ConsoleLogger : ILogger<string>
    {
        public void Log(string data)
        {
            Console.Write(data);
        }
    }
}
