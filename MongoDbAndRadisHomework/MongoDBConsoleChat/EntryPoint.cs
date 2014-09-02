namespace MongoDBConsoleChat
{
    using MongoDB.Driver;
    using System;
    using System.Linq;
    using MongoDB.Driver.Builders;
    using System.Threading;

    public class EntryPoint
    {
        private static MongoClient mongoClient;
        private static MongoServer mongoServer;
        private static MongoDatabase mongoDatabase;

        public static void Main()
        {
            InitConnection("ChatSystem");
            ShowMenu(true);

            while (true)
            {
                ExecuteCommands();
            }

        }

        public static void RegisterUser(string username)
        {
            var collectionInUse = mongoDatabase.GetCollection("Users");
            collectionInUse.Insert(new User(username));
        }

        public static void AddMessage(string message, string user)
        {
            var collectionInUse = mongoDatabase.GetCollection("Messages");
            collectionInUse.Insert(new Message(message, user, DateTime.Now));
        }

        public static void ListMessages(DateTime fromDate)
        {
            var collectionInUse = mongoDatabase.GetCollection("Messages");

            var searchQuery = Query.And(Query.GTE("DateAdded", fromDate));
            var allMessages = collectionInUse.FindAs<Message>(searchQuery);

            foreach (var message in allMessages)
            {
                Console.WriteLine(message);
                Console.WriteLine();
            }
        }

        public static void ShowChat(DateTime fromDate, string user)
        {
            while (true)
            {
                Console.Clear();
                ListMessages(fromDate);

                Console.WriteLine(GetSeparator());
                Console.WriteLine("Press any key to send message to chat!");
                Console.WriteLine("Type EXIT to exit the chat");
                Console.WriteLine(GetSeparator());

                if (Console.KeyAvailable == true)
                {
                    Console.Write("Your message: ");
                    var message = Console.ReadLine();

                    if (message == "EXIT")
                    {
                        break;
                    }

                    AddMessage(message, user);
                }

                Thread.Sleep(500);
            }
        }

        private static bool LogInUser(string username)
        {
            var collectionInUse = mongoDatabase.GetCollection("Users");

            var searchQuery = Query.And(Query.EQ("Username", username));
            var user = collectionInUse.FindOneAs<User>(searchQuery);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        private static void InitConnection(string database, string conStr = "mongodb://localhost/")
        {
            mongoClient = new MongoClient(conStr);
            mongoServer = mongoClient.GetServer();
            mongoDatabase = mongoServer.GetDatabase(database);
        }

        private static string GetSeparator(int separatorLength = 30)
        {
            return new string('-', separatorLength);
        }

        private static void ShowMenu(bool showWelcomeMessage = false)
        {
            if (showWelcomeMessage)
            {
                Console.WriteLine("Welcome to MongoDB Chat System");
            }

            Console.WriteLine(GetSeparator());
            Console.WriteLine("Type 1 register user.");
            Console.WriteLine("Type 2 to login and chat.");
            Console.WriteLine(GetSeparator());
        }

        private static void ExecuteCommands()
        {
            int command = int.Parse(Console.ReadLine());

            if (command == 1)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();

                RegisterUser(username);
            }

            if (command == 2)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();

                if (LogInUser(username))
                {
                    ShowChat(DateTime.Now, username);
                }
                else
                {
                    Console.WriteLine("This user does not exist.");
                }
            }

            ShowMenu();
        }
    }
}
