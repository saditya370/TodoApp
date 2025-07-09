using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Business.IServices;
using TodoApp.Business.ServiceModels;
using TodoApp.Data;
using TodoApp.Data.IRepository;

namespace TodoApp.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserServiceModels> CreateUser(string name, string Email , string FullNmae)
        {

            var user = new User();
            user.Name = name;
}
            return await _userRepository.CreateUser(user);
        }
    }
}
