using TodoApp.Business.ServiceModels;

namespace TodoApp.Business.IServices
{
    public interface IUserService
    {
        Task<UserServiceModels> CreateUser(string username, string email, string fullName);
        Task<UserServiceModels?> GetUserById(int id);
        Task<IEnumerable<UserServiceModels>> GetAllUsers();
        Task<UserServiceModels?> UpdateUser(int id, string username, string email, string fullName);
        Task<bool> DeleteUser(int id);
    }
}