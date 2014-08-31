namespace EntityFramework.Client
{
    using System;
    using System.Linq;
    using EntityFramework.Models;

    /// <summary>
    /// 2.
    /// Create a DAO class with static methods which provide functionality for inserting, modifying and deleting customers. 
    /// Write a testing class.
    /// </summary>
    public static class CustomersActions
    {
        public static void InsertCustomer(string customerID, string companyName, string contactName, string contactTitle)
        {
            var databaseConnection = new NorthwindEntities();

            var newCustomer = new Customer
            {
                CustomerID = customerID,
                CompanyName = companyName,
                ContactName = contactName,
                ContactTitle = contactTitle
            };

            using (databaseConnection)
            {
                databaseConnection.Customers.Add(newCustomer);
                databaseConnection.SaveChanges();
            }
        }

        public static void DeleteCustomer(string customerToEditID)
        {
            var databaseConnection = new NorthwindEntities();
            
            using (databaseConnection)
            {
                var customer = databaseConnection.Customers.FirstOrDefault(c => c.CustomerID == customerToEditID);

                if (customer != null)
                {
                    databaseConnection.Customers.Remove(customer);
                    databaseConnection.SaveChanges();
                }
            }
        }

        public static void UpdateCustomer(string customerToEditID, string newName)
        {
            var databaseConnection = new NorthwindEntities();

            using (databaseConnection)
            {
                var customer = databaseConnection.Customers.FirstOrDefault(c => c.CustomerID == customerToEditID);
                customer.ContactName = newName;

                databaseConnection.SaveChanges();
            }
        }
    }
}
