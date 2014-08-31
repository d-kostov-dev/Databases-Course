namespace TelerikAcademy.Client
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using TelerikAcademy.Data;

    public class EntryPoint
    {
        private static TelerikAcademyEntities databaseConnection;

        public static void Main()
        {
            Stopwatch timer = new Stopwatch();

            // Task 1
            databaseConnection = new TelerikAcademyEntities();
            using (databaseConnection)
            {
                timer.Restart();
                PrintEmployeesClean();
                Console.WriteLine("\nThe query for all employees WITHOUT include took: {0}.\nCheck screens in the solution folder for queries count!", timer.Elapsed);

                timer.Restart();
                PrintEmployeesIncluded();
                Console.WriteLine("\nThe query for all employees WITH include took: {0}.\nCheck screens in the solution folder for queries count!", timer.Elapsed);
            }

            // Task 2
            databaseConnection = new TelerikAcademyEntities();
            using (databaseConnection)
            {
                timer.Restart();
                GetEmployeesFromSofiaUnoptimized();
                Console.WriteLine("\nTo list operations took: {0} with N queries.", timer.Elapsed);

                timer.Restart();
                GetEmployeesFromSofiaOptimizedOne();
                Console.WriteLine("\nOptimized #1 query took: {0} with 1 querie.", timer.Elapsed);

                timer.Restart();
                GetEmployeesFromSofiaOptimizedTwo();
                Console.WriteLine("\nOptimized #2 query took: {0} with 1 querie.", timer.Elapsed);
            }
        }

        /// <summary>
        /// 1.
        /// Using Entity Framework write a SQL query to select all employees from the Telerik Academy database
        /// and later print their name, department and town. 
        /// Try the both variants: with and without .Include(…). 
        /// Compare the number of executed SQL statements and the performance.
        /// </summary>
        private static void PrintEmployeesClean()
        {
            var employees = databaseConnection.Employees;

            foreach (var emp in employees)
            {
                // Not printing the result we just need to calculate performance
                var result = string.Format(
                    "Name: {0}, Department: {1}, Town: {2}",
                    emp.FirstName + " " + emp.LastName,
                    emp.Department.Name,
                    emp.Address.Town.Name);
            }
        }

        private static void PrintEmployeesIncluded()
        {
            var employees = databaseConnection.Employees.Include("Department").Include("Address").Include("Address.Town");

            foreach (var emp in employees)
            {
                // Not printing the result we just need to calculate performance
                var result = string.Format(
                    "Name: {0}, Department: {1}, Town: {2}",
                    emp.FirstName + " " + emp.LastName,
                    emp.Department.Name,
                    emp.Address.Town.Name);
            }
        }

        /// <summary>
        /// 2.
        /// Using Entity Framework write a query that selects all employees from the Telerik Academy database, 
        /// then invokes ToList(), 
        /// then selects their addresses, then invokes ToList(), 
        /// then selects their towns, 
        /// then invokes ToList() 
        /// and finally checks whether the town is "Sofia". 
        /// Rewrite the same in more optimized way and compare the performance.
        /// </summary>
        private static void GetEmployeesFromSofiaUnoptimized()
        {
            var employeesFromSofia = databaseConnection.Employees.ToList()
                                    .Select(employee => employee.Address).ToList()
                                    .Select(address => address.Town).ToList()
                                    .Where(town => town.Name == "Sofia").ToList();
        }

        private static void GetEmployeesFromSofiaOptimizedOne()
        {
            var result =
                    databaseConnection.Employees
                    .Join(
                        databaseConnection.Addresses,
                        e => e.AddressID,
                        a => a.AddressID,
                        (e, a) => new { Employee = e, Address = a })
                    .Join(
                        databaseConnection.Towns,
                        emp => emp.Address.TownID,
                        town => town.TownID,
                        (emp, town) => new { emp, town })
                    .Where(x => x.town.Name == "Sofia")
                    .Select(x => x.emp.Employee.FirstName)
                    .ToList();
        }

        private static void GetEmployeesFromSofiaOptimizedTwo()
        {
            var employeesFromSofia = databaseConnection.Employees.Where(emp => emp.Address.Town.Name == "Sofia").ToList();
        }
    }
}
