using BlogApp.DTOs;
using BlogApp.Models;

namespace BlogApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> ValidateUserAsync(string email, string password);
        Task<User> RegisterUserAsync(string firstName, string lastName, string email, string password);
    }
}
