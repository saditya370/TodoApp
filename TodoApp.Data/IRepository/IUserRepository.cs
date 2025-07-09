using TodoApp.Data.Entities;

namespace TodoApp.Data.IRepository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
    }
}