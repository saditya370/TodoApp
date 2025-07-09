using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.TodosDtos
{

    public class CreateTodoDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Priority { get; set; } = 1;

        public string Category { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class UpdateTodoDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Priority { get; set; } = 1;

        public string Category { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
