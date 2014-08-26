namespace Northwind
{
    using System;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    
    public class EntryPoint
    {
        private static SqlConnection databaseConnection;
        private static SqlCommand commandToExecute;
        private static SqlDataReader reader;
        private static string sqlCommand;

        public static void Main()
        {
            databaseConnection = new SqlConnection(
                "Server=.\\NEWSQL; " +
                "Database=Northwind; " +
                "Integrated Security=true");

            databaseConnection.Open();

            using (databaseConnection)
            {
                ShowCategoriesCount();
                ConsoleUISeparator();

                ShowCategoriesData();
                ConsoleUISeparator();

                ShowProductsInCategory();
                ConsoleUISeparator();

                AddProduct("Pesho", 0.99);

                SaveAllImages();
                ConsoleUISeparator();

                Console.WriteLine("Enter product name to search:");
                string productToSearch = Console.ReadLine();

                ProductSearch(productToSearch);
            }
        }

        /// <summary>
        /// 1.
        /// Write a program that retrieves from the Northwind sample database in MS SQL Server the number of rows 
        /// in the Categories table.
        /// </summary>
        private static void ShowCategoriesCount()
        {
            sqlCommand = "SELECT COUNT(*) FROM Categories";
            commandToExecute = new SqlCommand(sqlCommand, databaseConnection);
            int categoriesCount = (int)commandToExecute.ExecuteScalar();
            Console.WriteLine("Categories count: {0} ", categoriesCount);
        }

        /// <summary>
        /// 2.
        /// Write a program that retrieves the name and description of all categories in the Northwind DB.
        /// </summary>
        private static void ShowCategoriesData()
        {
            sqlCommand = "SELECT CategoryName, Description FROM Categories";
            commandToExecute = new SqlCommand(sqlCommand, databaseConnection);
            reader = commandToExecute.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];
                    string description = (string)reader["Description"];

                    Console.WriteLine("Category: {0}\n\t{1}\n", categoryName, description);
                }
            }
        }

        /// <summary>
        /// 3.
        /// Write a program that retrieves from the Northwind database all product categories 
        /// and the names of the products in each category. 
        /// Can you do this with a single SQL query (with table join)?
        /// </summary>
        private static void ShowProductsInCategory()
        {
            sqlCommand = "SELECT c.CategoryName, " +
                               "STUFF((SELECT ', ' + p.ProductName " +
                                        "FROM Products p " +
                                        "WHERE p.CategoryID = c.CategoryID " +
                                        "FOR XML PATH('')), 1, 1, '') AS Products " +
                        "FROM Categories c";

            commandToExecute = new SqlCommand(sqlCommand, databaseConnection);
            reader = commandToExecute.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];
                    string products = (string)reader["Products"];

                    Console.WriteLine("Category: {0}\n\t{1}\n", categoryName, products);
                }
            }
        }

        /// <summary>
        /// 4.
        /// Write a method that adds a new product in the products table in the Northwind database. 
        /// Use a parameterized SQL command.
        /// </summary>
        private static void AddProduct(
            string productName,
            double price,
            int supplierID = 1,
            int categoryID = 1,
            bool discontinued = false)
        {
            sqlCommand = "INSERT INTO Products(ProductName, UnitPrice, SupplierID, CategoryID, Discontinued) " +
                "VALUES (@productName, @price, @supplierID, @categoryID, @discontinued)";

            commandToExecute = new SqlCommand(sqlCommand, databaseConnection);

            commandToExecute.Parameters.AddWithValue("@productName", productName);
            commandToExecute.Parameters.AddWithValue("@price", price);
            commandToExecute.Parameters.AddWithValue("@supplierID", supplierID);
            commandToExecute.Parameters.AddWithValue("@categoryID", categoryID);
            commandToExecute.Parameters.AddWithValue("@discontinued", discontinued);

            commandToExecute.ExecuteNonQuery();
        }

        /// <summary>
        /// 5.
        /// Write a program that retrieves the images for all categories in the Northwind database 
        /// and stores them as JPG files in the file system.
        /// </summary>
        private static void SaveAllImages()
        {
            commandToExecute = new SqlCommand(
                                    "SELECT CategoryName, Picture " +
                                    "FROM Categories", 
                                    databaseConnection);

            reader = commandToExecute.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string categoryName = (string)reader["CategoryName"];

                    if (categoryName.Contains("/") == true)
                    {
                        categoryName = categoryName.Replace('/', ' ');
                    }

                    byte[] pictureBytes = reader["Picture"] as byte[];
                    var metaFilePictSize = 78;

                    MemoryStream imageStream = new MemoryStream(pictureBytes, metaFilePictSize, pictureBytes.Length - metaFilePictSize);

                    Image image = Image.FromStream(imageStream);

                    using (image)
                    {
                        image.Save(categoryName + ".jpg", ImageFormat.Jpeg);
                    }
                }

                Console.WriteLine("You can find the pictures in the bin/debug folder.");
            }
        }

        /// <summary>
        /// 8.
        /// Write a program that reads a string from the console and finds all products that contain this string. 
        /// Ensure you handle correctly characters like:
        /// ', %, ", \ and _.
        /// </summary>
        private static void ProductSearch(string searchQuery)
        {
            sqlCommand = "SELECT ProductName FROM Products WHERE ProductName LIKE '%" + searchQuery + "%'";
            commandToExecute = new SqlCommand(sqlCommand, databaseConnection);
            reader = commandToExecute.ExecuteReader();
            Console.WriteLine("Search results:");
            using (reader)
            {
                while (reader.Read())
                {
                    string productName = (string)reader["ProductName"];
                    Console.WriteLine(productName);
                }
            }
        }

        /// <summary>
        /// Shows a line in the console to separate different tasks for "better" UI.
        /// </summary>
        private static void ConsoleUISeparator()
        {
            Console.WriteLine(new string('-', 20));
        }
    }
}
