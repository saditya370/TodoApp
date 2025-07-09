using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }
    }
}