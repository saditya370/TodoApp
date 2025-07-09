using TodoApp.Data.Entities;

namespace TodoApp.Data.IRepository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User?> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User?> GetUserByUsername(string username);

    }
}