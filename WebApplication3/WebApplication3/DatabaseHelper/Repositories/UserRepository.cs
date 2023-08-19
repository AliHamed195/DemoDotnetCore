using Microsoft.AspNetCore.SignalR;
using MySqlConnector;
using System.Data;
using WebApplication3.DatabaseHelper.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.DatabaseHelper.Repositories
{
    /// <summary>
    /// Represents a repository for managing users using a MySQL database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default") 
                ?? throw new InvalidOperationException("The 'Default' connection string is missing in configuration.");
        }

        /// <inheritdoc/>
        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new();

            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new("getAllUsers", connection);
            command.CommandType = CommandType.StoredProcedure;

            using MySqlDataReader reader = command.ExecuteReader();

            // Convert the table to a list of type User
            while (reader.Read())
            {
                users.Add(new User
                {
                    User_id = reader.GetInt32("user_id"),
                    Name = reader.GetString("name"),
                    Age = reader.GetInt32("age")
                });
            }

            connection.Close();
            
            return users;
        }

        /// <inheritdoc/>
        public void CreateUser(string? name, int? age)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();

            using MySqlCommand command = new("createUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@p_name", name);
            command.Parameters.AddWithValue("@p_age", age);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
