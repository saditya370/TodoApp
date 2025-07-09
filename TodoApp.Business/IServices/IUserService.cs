using TodoApp.Business.ServiceModels;

namespace TodoApp.Business.IServices
{
    public interface IUserService
    {

     Task<UserServiceModels> CreateUser(string userName, string email, string password);

    }
}