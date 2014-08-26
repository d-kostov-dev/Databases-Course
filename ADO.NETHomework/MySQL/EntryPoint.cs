namespace MySQL
{
    using System;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// Download and install MySQL database, MySQL Connector/Net (.NET Data Provider for MySQL) 
    /// + MySQL Workbench GUI administration tool. 
    /// Create a MySQL database to store Books (title, author, publish date and ISBN). 
    /// Write methods for listing all books, finding a book by name and adding a book.
    /// </summary>
    public class EntryPoint
    {
        private static MySqlConnection mySQLConnection;

        public static void Main()
        {
            var connectionString = "Server=localhost;Database=bookstore;Uid=root;Pwd=;pooling=true;";
            mySQLConnection = new MySqlConnection(connectionString);

            mySQLConnection.Open();
            using (mySQLConnection)
            {
                // Add books
                AddBook("Pesho", "Gosho", DateTime.Now, 12345679);
                AddBook("Stamat", "Kencho", DateTime.Now, 45645757);
                AddBook("Super Kencho", "I.K.", DateTime.Now, 78978977);
                AddBook("Zadushaam Sa", "Bat Joro", DateTime.Now, 45123423);

                // List all books
                ListAllBooks();

                // Find book by name
                ShowBookByTitle("Zadushaam Sa");

                // Clears the table so i don't have 1231231 "Pesho" titles! :)
                DeleteAllBooks();
            }
        }

        private static void ListAllBooks()
        {
            string sqlCommand = "SELECT * FROM books";
            var commandToExecute = new MySqlCommand(sqlCommand, mySQLConnection);
            var reader = commandToExecute.ExecuteReader();

            UseGivenReader(reader);
        }

        private static void ShowBookByTitle(string bookTitle)
        {
            string sqlCommand = "SELECT * FROM books WHERE title LIKE '%" + bookTitle + "%'";
            var commandToExecute = new MySqlCommand(sqlCommand, mySQLConnection);
            var reader = commandToExecute.ExecuteReader();

            UseGivenReader(reader);
        }

        private static void AddBook(string title, string author, DateTime publishDate, int isbn)
        {
            string sqlCommand = 
                "INSERT INTO books(title, author, publish_date, isbn) " +
                "VALUES (@title, @author, @publishDate, @isbn)";

            var commandToExecute = new MySqlCommand(sqlCommand, mySQLConnection);

            commandToExecute.Parameters.AddWithValue("@title", title);
            commandToExecute.Parameters.AddWithValue("@author", author);
            commandToExecute.Parameters.AddWithValue("@publishDate", publishDate);
            commandToExecute.Parameters.AddWithValue("@isbn", isbn);

            commandToExecute.ExecuteNonQuery();
        }

        private static void UseGivenReader(MySqlDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    string title = (string)reader["title"];
                    string author = (string)reader["author"];

                    Console.WriteLine("Title: {0}\nAuthor:{1}\n", title, author);
                }
            }
        }

        private static void DeleteAllBooks()
        {
            string sqlCommand = "DELETE FROM books";
            var commandToExecute = new MySqlCommand(sqlCommand, mySQLConnection);
            commandToExecute.ExecuteNonQuery();
        }
    }
}
