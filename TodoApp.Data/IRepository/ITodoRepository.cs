using TodoApp.Data.Entities;

namespace TodoApp.Data.IRepository
{
    public interface ITodoRepository
    {
        Task<Todo> CreateTodo(Todo todo);
        Task<Todo?> GetTodoById(int id);
        Task<IEnumerable<Todo>> GetAllTodos();
        Task<IEnumerable<Todo>> GetTodosByUserId(int userId);
        Task<Todo> UpdateTodo(Todo todo);
        Task<bool> DeleteTodo(int id);
        Task<IEnumerable<Todo>> GetTodosByCategory(string category);
        Task<IEnumerable<Todo>> GetTodosByPriority(int priority);
        Task<IEnumerable<Todo>> SearchTodos(string query);
        Task<IEnumerable<Todo>> GetOverdueTodos();
    }
}