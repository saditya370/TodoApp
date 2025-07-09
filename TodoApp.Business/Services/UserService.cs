using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Business.IServices;
using TodoApp.Business.ServiceModels;
using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Data.IRepository;
using TodoApp.UtilitiesAndConstants;

namespace TodoApp.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<UserServiceModels> CreateUser(string username, string email, string fullName, string Password)
        {
            var hashedPassword = PasswordHasher.Hash(Password);
            var user = new User
            {
                Username = username,
                Email = email,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = hashedPassword

            };

            var createdUser = await _userRepository.CreateUser(user);

            return new UserServiceModels
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                FullName = createdUser.FullName
            };
        }

        public async Task<UserServiceModels?> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return null;

            return new UserServiceModels
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<IEnumerable<UserServiceModels>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(u => new UserServiceModels
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FullName = u.FullName
            });
        }

        public async Task<UserServiceModels?> UpdateUser(int id, string username, string email, string fullName)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null) return null;

            existingUser.Username = username;
            existingUser.Email = email;
            existingUser.FullName = fullName;

            var updatedUser = await _userRepository.UpdateUser(existingUser);

            return new UserServiceModels
            {
                Id = updatedUser.Id,
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                FullName = updatedUser.FullName
            };
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return _jwtService.GenerateToken(user.Id, user.Username);
        }
    }
}
