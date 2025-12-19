using System;
using MySql.Data.MySqlClient;
// namespace DatabaseConnection
// {
    class ConnectDatabaseCreatetable
    {
        public static void ConnectDb()
        {
            // Connection string for the MySQL server (without database name)
            string serverConnectionString = "server=localhost;user=root;password=;port=3306;";

            // Name of the database to create
            string databaseName = "ecommerce_db";

            // Connection string for the database (after it's created)
            string databaseConnectionString = $"{serverConnectionString}database={databaseName};";

            // Create a connection to the MySQL server
            using (MySqlConnection connection = new MySqlConnection(serverConnectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    Console.WriteLine("Connected to the MySQL server.");

                    // Create the database if it doesn't exist
                    CreateDatabase(connection, databaseName);

                    // Switch to the newly created database
                    connection.ChangeDatabase(databaseName);
                    Console.WriteLine($"Using database: {databaseName}");

                    // Create the users table if it doesn't exist
                    CreateUserTable(connection);
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

        static void CreateDatabase(MySqlConnection connection, string databaseName)
        {
            // SQL query to create the database if it doesn't exist
            string query = $"CREATE DATABASE IF NOT EXISTS {databaseName};";

            // Create a command object
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Execute the command
                command.ExecuteNonQuery();
                Console.WriteLine($"Database '{databaseName}' created or already exists.");
            }
        }

        static void CreateUserTable(MySqlConnection connection)
        {
            // SQL query to create the users table if it doesn't exist
            string query = @"
            CREATE TABLE IF NOT EXISTS users (
                user_id INT AUTO_INCREMENT PRIMARY KEY,
                username VARCHAR(50) NOT NULL,
                email VARCHAR(100) NOT NULL UNIQUE,
                password_hash VARCHAR(255) NOT NULL,
                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
            );";

            // Create a command object
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Execute the command
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'users' created or already exists.");
            }
        }
    }
// }



    

   