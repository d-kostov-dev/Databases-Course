namespace MongoDbDictionary
{
    using System;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    /// <summary>
    /// Write a simple "Dictionary" application in C# or JavaScript to perform the following in MongoDB: 
    ///  - Add a dictionary entry (word + translation)
    ///  - List all words and their translations
    ///  - Find the translation of given word
    /// </summary>
    public class EntryPoint
    {
        private static MongoClient mongoClient;
        private static MongoServer mongoServer;
        private static MongoDatabase mongoDatabase;
        private static MongoCollection collectionInUse;

        public static void Main()
        {
            InitConnection("Dictionary", "Words");
            ShowMenu(true);

            while (true)
            {
                ExecuteCommands();
            }
        }

        public static void AddWord(string wordValue, string wordTranslation)
        {
            collectionInUse.Insert(new DictionaryWord(wordValue, wordTranslation));
        }

        public static void ListAllWords()
        {
            var allWords = collectionInUse.FindAllAs<DictionaryWord>();

            foreach (var word in allWords)
            {
                Console.WriteLine(word);
            }
        }

        public static void SearchInDictionary(string wordToSearch)
        {
            var searchQuery = Query.And(Query.EQ("Value", wordToSearch));
            var filteredWords = collectionInUse.FindAs<DictionaryWord>(searchQuery);

            Console.WriteLine("Results for: {0}\n", wordToSearch);
            foreach (var word in filteredWords)
            {
                Console.WriteLine(word.ToString());
            }
        }

        private static void ShowMenu(bool showWelcomeMessage = false)
        {
            if (showWelcomeMessage)
            {
                Console.WriteLine("Welcome to MongoDB Dictionary");
            }

            Console.WriteLine(GetSeparator());
            Console.WriteLine("Type 1 to add word in the dictionary database.");
            Console.WriteLine("Type 2 to see a list of all words in the database.");
            Console.WriteLine("Type 3 to search for a word in the database.");
            Console.WriteLine(GetSeparator());
        }

        private static void ExecuteCommands()
        {
            int command = int.Parse(Console.ReadLine());

            if (command == 1)
            {
                Console.Write("Word: ");
                string wordToAdd = Console.ReadLine();

                Console.Write("Translation: ");
                string wordTranslation = Console.ReadLine();

                AddWord(wordToAdd, wordTranslation);
            }

            if (command == 2)
            {
                ListAllWords();
            }

            if (command == 3)
            {
                Console.Write("Search for: ");
                string searchWord = Console.ReadLine();
                SearchInDictionary(searchWord);
            }

            ShowMenu();
        }

        private static void InitConnection(string database, string collection, string conStr = "mongodb://localhost/")
        {
            mongoClient = new MongoClient(conStr);
            mongoServer = mongoClient.GetServer();
            mongoDatabase = mongoServer.GetDatabase(database);
            collectionInUse = mongoDatabase.GetCollection(collection);
        }

        private static string GetSeparator(int separatorLength = 30)
        {
            return new string('-', separatorLength);
        }
    }
}
