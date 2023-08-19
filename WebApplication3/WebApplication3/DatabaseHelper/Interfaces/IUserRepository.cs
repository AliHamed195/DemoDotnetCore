using WebApplication3.Models;

namespace WebApplication3.DatabaseHelper.Interfaces
{
    /// <summary>
    /// Represents a repository for managing users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>An enumerable collection of User objects contains [user_id, name, age].</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Creates a new user with the specified name and age.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="age">The age of the user.</param>
        void CreateUser(string? name, int? age);
    }
}
