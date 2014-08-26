namespace Excel
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    /// <summary>
    /// Create an Excel file with 2 columns: name and score:
    /// Write a program that reads your MS Excel file through the OLE DB data provider 
    /// and displays the name and score row by row.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            OleDbConnection excelConnection = new OleDbConnection(
                "Provider=Microsoft.ACE.OLEDB.12.0;" +
                "Data Source=../../studentsData.xlsx;" +
                "Extended Properties=Excel 12.0;");

            excelConnection.Open();
            using (excelConnection)
            {
                // Testing some inserting
                OleDbCommand insertCommand = new OleDbCommand(
                    "INSERT INTO [MyTable$] (Name, Score) " +
                    "VALUES (@Name , @Score)",
                    excelConnection);

                insertCommand.Parameters.AddWithValue("@Name", "Stamat");
                insertCommand.Parameters.AddWithValue("@Score", "40");

                insertCommand.ExecuteNonQuery();

                // Getting the data
                OleDbCommand command = new OleDbCommand("SELECT * FROM [MyTable$] ", excelConnection);
                OleDbDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string studentName = (string)reader["Name"];
                        double studentScore = (double)reader["Score"];
                        Console.WriteLine("Student: {0} - Score: {1}", studentName, studentScore);
                    }
                }
            }
        }
    }
}
