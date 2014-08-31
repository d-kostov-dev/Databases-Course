namespace EntityFramework.Client
{
    using System;
    using System.Linq;
    using EntityFramework.Models;
    using System.Data.SqlClient;

    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Get orders using LINQ.");
            Console.WriteLine("--------");
            ShowOrdersByCountryAndDate("Canada", new DateTime(1997, 1, 1));

            Console.WriteLine();

            Console.WriteLine("Get orders using native SQL.");
            Console.WriteLine("--------");
            ShowOrdersByCountryAndDateClassic("Canada", new DateTime(1997, 1, 1));

            Console.WriteLine();

            Console.WriteLine("Get orders by region and date.");
            Console.WriteLine("--------");
            FindAllOrdersByRegionAndDate("BC", new DateTime(1997, 1, 1), new DateTime(1997, 12, 31));

            Console.WriteLine();

            Console.WriteLine("Adding order with few items.");
            Console.WriteLine("--------");
            AddOrder();

            Console.WriteLine();

            Console.WriteLine("Get employee territories.");
            Console.WriteLine("--------");
            GetEmployeeTerritories();
            
            Console.WriteLine();

            Console.WriteLine("Get Supplier Income");
            Console.WriteLine("--------");
            GetTotalSupplierIncome("Exotic Liquids", new DateTime(1996, 1, 1), new DateTime(1998, 12, 31));
        }

        /// <summary>
        /// 2.
        /// Create a DAO class with static methods which provide functionality for inserting, modifying and deleting customers. 
        /// Write a testing class.
        /// </summary>
        private static void TestCustomersActionsFunctionality()
        {
            CustomersActions.InsertCustomer("PESCO", "PeshoCompany", "Pesho Goshov", "MC");
            CustomersActions.UpdateCustomer("PESCO", "Pesho Stamatov Goshov");
            CustomersActions.DeleteCustomer("PESCO");
        }

        /// <summary>
        /// 3.
        /// Write a method that finds all customers who have orders made in 1997 and shipped to Canada.
        /// </summary>
        private static void ShowOrdersByCountryAndDate(string country, DateTime date)
        {
            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                var result =
                    databaseConnection.Customers
                    .Join(
                        databaseConnection.Orders,
                        c => c.CustomerID,
                        o => o.CustomerID,
                        (c, o) => new { Customer = c, Order = o })
                    .Where(x => x.Order.ShipCountry == country)
                    .Where(x => x.Order.OrderDate.Value.Year == date.Year)
                    .Select(x => x.Customer);

                foreach (var item in result)
                {
                    Console.WriteLine(item.ContactName);
                }
            }
        }

        /// <summary>
        /// 4.
        /// Implement previous by using native SQL query and wxecuting it through the DbContext
        /// </summary>
        private static void ShowOrdersByCountryAndDateClassic(string country, DateTime date)
        {
            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                string nativeSqlQuery =
                    "SELECT c.ContactName " +
                    "FROM Customers c " +
                    "INNER JOIN Orders o ON c.CustomerID = o.CustomerID " +
                    "WHERE o.ShipCountry = {0} " +
                    "AND YEAR(o.OrderDate) = {1}";

                object[] parameters = { country, date.Year };
                var resultingCustomers = databaseConnection.Database.SqlQuery<string>(nativeSqlQuery, parameters);

                foreach (var cust in resultingCustomers)
                {
                    Console.WriteLine(cust);
                }
            }
        }

        /// <summary>
        /// 5.
        /// Write a method that finds all the sales by specified region and period (start / end dates).
        /// </summary>
        private static void FindAllOrdersByRegionAndDate(string region, DateTime startDate, DateTime endDate)
        {
            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                var result =
                    databaseConnection.Orders
                    .Where(o => o.ShipRegion == region &&
                                o.OrderDate >= startDate &&
                                o.OrderDate <= endDate)
                    .Select(o => o.OrderID);

                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// 8.
        /// By inheriting the Employee entity class create a class which allows 
        /// employees to access their corresponding territories as property of type EntitySet<T>.
        /// </summary>
        private static void GetEmployeeTerritories()
        {
            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                var result = databaseConnection.Employees;

                foreach (var item in result)
                {
                    foreach (var territory in item.GetTerritories)
                    {
                        Console.WriteLine(territory.TerritoryDescription);
                    }
                }
            }
        }

        /// <summary>
        /// 9.
        /// Create a method that places a new order in the Northwind database. 
        /// The order should contain several order items. 
        /// Use transaction to ensure the data consistency.
        /// </summary>
        private static void AddOrder()
        {
            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                using (var dbContextTransaction = databaseConnection.Database.BeginTransaction())
                {
                    try
                    {
                        var newOrder = new Order()
                        {
                            CustomerID = "BOTTM",
                            EmployeeID = 5,
                            OrderDate = DateTime.Now
                        };

                        newOrder.Order_Details.Add(new Order_Detail { ProductID = 11, UnitPrice = 14.00m, Quantity = 12, Discount = 0 });
                        newOrder.Order_Details.Add(new Order_Detail { ProductID = 42, UnitPrice = 9.80m, Quantity = 5, Discount = 0 });
                        newOrder.Order_Details.Add(new Order_Detail { ProductID = 72, UnitPrice = 34.50m, Quantity = 7, Discount = 0 });
                         
                        databaseConnection.Orders.Add(newOrder);
                        databaseConnection.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// 10.
        /// Create a stored procedures in the Northwind database for finding the total incomes 
        /// for given supplier name and period (start date, end date).
        /// Implement a C# method that calls the stored procedure and returns the retuned record set.
        /// </summary>
        private static void GetTotalSupplierIncome(string supplierName, DateTime startDate, DateTime endDate)
        {

            //CREATE PROC [dbo].[usp_GetSupplierIncome] (@supplierName nvarchar(max),  @startYear int, @endYear int)
            //AS
            //    SELECT SUM(od.Quantity * od.UnitPrice) AS TotalIncome
            //    FROM Suppliers s
            //        INNER JOIN Products p
            //            ON s.SupplierID = p.SupplierID
            //        INNER JOIN [ORDER Details] od
            //            ON od.ProductID = p.ProductID
            //        INNER JOIN Orders o
            //            ON od.OrderID = o.OrderID
            //    WHERE s.CompanyName = @supplierName 
            //    AND YEAR(o.OrderDate) >= @startYear 
            //    AND YEAR(o.OrderDate) <= @endYear
            //GO

            //EXEC dbo.GetSupplierIncome 'Exotic Liquids', 1996, 1998

            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                var income = databaseConnection.usp_GetSupplierIncome(supplierName, startDate.Year, endDate.Year).ToList();

                Console.WriteLine(income[0]);
            }    
        }
    }
}
