using TodoApp.Business.ServiceModels;

namespace TodoApp.Business.IServices
{
    public interface ITodoService
    {
        Task<TodoServiceModels> CreateTodo(string title, string description, int priority, string category, DateTime dueDate, int userId);
        Task<TodoServiceModels?> GetTodoById(int id);
        Task<IEnumerable<TodoServiceModels>> GetAllTodos();
        Task<IEnumerable<TodoServiceModels>> GetTodosByUserId(int userId);
        Task<TodoServiceModels?> UpdateTodo(int id, string title, string description, int priority, string category, DateTime dueDate, bool isCompleted);
        Task<bool> DeleteTodo(int id);
        Task<bool> MarkTodoAsCompleted(int id);
        Task<IEnumerable<TodoServiceModels>> GetTodosByCategory(string category);
        Task<IEnumerable<TodoServiceModels>> GetTodosByPriority(int priority);
      

        Task <IEnumerable<TodoServiceModels>> SearchTodos(string query);
        Task<IEnumerable<TodoServiceModels>> GetOverdueTodos();
    }
}