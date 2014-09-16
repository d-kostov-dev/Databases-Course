namespace CompanyEmployees.ConsoleClient.RandomGenerators
{
    using System;
    using System.Collections.Generic;

    using CompanyEmployees.ConsoleClient.Interfaces;

    public sealed class ValuesGenerator : IRandomValueGenerator
    {
        // I know i know the names of the constats suck, but it's easier to read this way in the code. Try it!
        private const int DEFAULT_MINIMAL_STRING_LENGTH = 10;
        private const int DEFAULT_MAXIMAL_STRING_LENGTH = 20;
        private const string RANDOM_STRING_BASE_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private static readonly Random SystemRandom = new Random();

        private static IRandomValueGenerator randomGeneratorInstance;

        private ValuesGenerator()
        {
        }

        /// <summary>
        /// Gets the instance of the class. Singleton Design Pattern
        /// </summary>
        public static IRandomValueGenerator GetInstance
        {
            get
            {
                if (randomGeneratorInstance == null)
                {
                    randomGeneratorInstance = new ValuesGenerator();
                }

                return randomGeneratorInstance;
            }
        }

        /// <summary>
        /// Generates random integer number.
        /// </summary>
        public int GetRandomInt(int minValue = 0, int maxValue = int.MaxValue)
        {
            int generatedNumber = SystemRandom.Next(minValue, maxValue + 1);

            return generatedNumber;
        }

        /// <summary>
        /// Generates random double number in given range. Default ranges are 0.00 to double.MaxValue.
        /// </summary>
        public double GetRandomDouble(double minValue = 0.00, double maxValue = double.MaxValue)
        {
            // Returns a random floating-point number between 0.0 and 1.0.
            var randomDoubleValue = SystemRandom.NextDouble();
            var generatedNumber = minValue + (randomDoubleValue * (maxValue - minValue));

            return generatedNumber;
        }

        /// <summary>
        /// Generates random string with given lengh. Default length is 10.
        /// </summary>
        public string GetRandomString(int length = DEFAULT_MAXIMAL_STRING_LENGTH)
        {
            char[] charsSelected = new char[length];

            for (int i = 0; i < length; i++)
            {
                charsSelected[i] = RANDOM_STRING_BASE_CHARS[SystemRandom.Next(RANDOM_STRING_BASE_CHARS.Length)];
            }

            return new string(charsSelected);
        }

        /// <summary>
        /// Generates random string with random lenght between 3 and 10 symbols.
        /// </summary>
        public string GetRandomLengthString()
        {
            int randomLength = this.GetRandomInt(DEFAULT_MINIMAL_STRING_LENGTH, DEFAULT_MAXIMAL_STRING_LENGTH);
            string generatedString = this.GetRandomString(randomLength);

            return generatedString;
        }

        /// <summary>
        /// Generates a set (with given length) of random strings (optional lenght). 
        /// Default strings length is between 3 and 10 symbols.
        /// </summary>
        public ISet<string> GetUniqueRandomStringsSet(int listLength)
        {
            var generatedStrings = new HashSet<string>();

            while (generatedStrings.Count < listLength)
            {
                generatedStrings.Add(this.GetRandomLengthString());
            }

            return generatedStrings;
        }

        /// <summary>
        /// Generates a set (with given length) of random integers.
        /// </summary>
        public ISet<int> GetUniqueRandomIntegersSet(int listLength, int minValue = 0, int maxValue = int.MaxValue)
        {
            var generatedIntegers = new HashSet<int>();

            while (generatedIntegers.Count < listLength)
            {
                int currentInteger = this.GetRandomInt(minValue, maxValue);
                generatedIntegers.Add(currentInteger);
            }

            return generatedIntegers;
        }

        /// <summary>
        /// Generates random date from minimal year to today. Default minimal date is 1990.1.1
        /// </summary>
        public DateTime GetRandomDate(int minimalYear = 1990)
        {
            ////DateTime startDate = new DateTime(minimalYear, 1, 1);
            ////int range = (DateTime.Today - startDate).Days;
            ////var generatedDate = startDate.AddDays(SystemRandom.Next(range));
            var year = this.GetRandomInt(2000, 2050);
            var day = this.GetRandomInt(1, 30);
            var month = this.GetRandomInt(3, 12);
            var generatedDate = new DateTime(year, month, day);
            return generatedDate;
        }
    }
}
