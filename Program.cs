
using System;
using MySql.Data.MySqlClient;

public class Program
{
    public static void Main(string[] args)
    {


        string connectionString = "server=localhost;user=root;port=3306;password=;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Console.WriteLine("Connected to MySQL database!");
                // ConnectDatabaseCreatetable.ConnectDb();
                Console.WriteLine("Do You want to Insert User or Retrive Data(insert/retrive): ");
                string 
                InsertDataInTable.InsertData();
                ReadWriteData.ReadWrite();
                
                // Perform database operations here
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
            }
        }
    }
}