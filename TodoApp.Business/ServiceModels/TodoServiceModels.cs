namespace TodoApp.Business.ServiceModels
{
    public class TodoServiceModels
    {
       
       public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Priority { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;

    }
}