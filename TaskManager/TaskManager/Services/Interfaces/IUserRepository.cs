using TaskManager.Data;

namespace TaskManager.Services.Interfaces {
    public interface IUserRepository {
        Task<int> CreateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUserNameAsync(string username);
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(User user);
    }
}