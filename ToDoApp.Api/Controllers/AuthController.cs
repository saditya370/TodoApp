using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Business.IServices;
using TodoApp.Business.Services;
using ToDoApp.Api.UserDtos;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var token = await _userService.LoginAsync(request.Username, request.Password);
            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }



    }
}

