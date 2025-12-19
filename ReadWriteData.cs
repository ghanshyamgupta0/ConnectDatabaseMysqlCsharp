using System;
using MySql.Data.MySqlClient;

// namespace DatabaseConnection
// {
    class ReadWriteData
    {
        public static void ReadWrite()
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
                    Console.WriteLine("Connected to the database.");

                    // Insert a new user
                    // InsertUser(connection, "john_doe", "john.doe@example.com", "hashed_password_123");

                    // Retrieve all users
                    Console.WriteLine("\nAll Users:");
                    RetrieveAllUsers(connection);

                    // Retrieve a specific user by user_id
                    Console.WriteLine("\nUser with ID 1:");
                    RetrieveUserById(connection, 1);
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
                        Console.WriteLine("\nConnection closed.");
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

        static void RetrieveAllUsers(MySqlConnection connection)
        {
            // SQL query to retrieve all users
            string query = "SELECT * FROM users;";

            // Create a command object
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Execute the query and get a data reader
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Read the data
                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["user_id"]}, Username: {reader["username"]}, Email: {reader["email"]}, Created At: {reader["created_at"]}");
                    }
                }
            }
        }

        static void RetrieveUserById(MySqlConnection connection, int userId)
        {
            // SQL query to retrieve a specific user by user_id
            string query = "SELECT * FROM users WHERE user_id = @userId;";

            // Create a command object
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // Add parameters to the query
                command.Parameters.AddWithValue("@userId", userId);

                // Execute the query and get a data reader
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Read the data
                    if (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["user_id"]}, Username: {reader["username"]}, Email: {reader["email"]}, Created At: {reader["created_at"]}");
                    }
                    else
                    {
                        Console.WriteLine($"User with ID {userId} not found.");
                    }
                }
            }
        }
    }
// }