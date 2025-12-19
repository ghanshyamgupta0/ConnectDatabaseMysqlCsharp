using System;
using MySql.Data.MySqlClient;

// namespace DatabaseConnection
// {
    class InsertDataInTable
    {
        public static void InsertData()
        {
            // Connection string for the database
            string connectionString = "server=localhost;user=root;password=;database=ecommerce_db;port=3306;";

            // Create a connection object
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    // Console.WriteLine("Connected to the database.");

                    Console.WriteLine("Enter how many user you want to enter: ");
                    int n = int.Parse(Console.ReadLine());
                    for (int i = 0; i <= n; i++)
                    {
                        
                    Console.WriteLine("Enter the name of the user: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter the email of the user: ");
                    string email = Console.ReadLine();
                    // Insert a new user
                    InsertUser(connection, name, email, "hashed_password_123");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
        }

        static void InsertUser(MySqlConnection connection, string username, string email, string passwordHash)
        {
            // SQL query to insert a new user
            string query = @"
                INSERT INTO users (username, email, password_hash)
                VALUES (@username, @email, @passwordHash);";

            // Create a command object
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Add parameters to the query
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@passwordHash", passwordHash);

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) inserted.");
            }
        }
    }
// }

