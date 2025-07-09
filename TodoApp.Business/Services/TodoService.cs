using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Business.IServices;
using TodoApp.Business.ServiceModels;
using TodoApp.Data.Entities;
using TodoApp.Data.IRepository;

namespace TodoApp.Business.Services
{
  public class TodoService : ITodoService
    {
        private readonly Data.IRepository.ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoServiceModels> CreateTodo(string title, string description, int priority, string category, DateTime dueDate, int userId)
        {
            var todo = new Todo
            {
                Title = title,
                Description = description,
                Priority = priority,
                Category = category,
                DueDate = dueDate,
                UserId = userId,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            var createdTodo = await _todoRepository.CreateTodo(todo);
            
            return new TodoServiceModels
            {
                Id = createdTodo.Id,
                Title = createdTodo.Title,
                Description = createdTodo.Description,
                IsCompleted = createdTodo.IsCompleted,
                CreatedAt = createdTodo.CreatedAt,
                CompletedAt = createdTodo.CompletedAt,
                Priority = createdTodo.Priority,
                Category = createdTodo.Category,
                DueDate = createdTodo.DueDate,
                UserId = createdTodo.UserId,
                Username = createdTodo.User?.Username ?? string.Empty
            };
        }

        public async Task<TodoServiceModels?> GetTodoById(int id)
        {
            var todo = await _todoRepository.GetTodoById(id);
            if (todo == null) return null;

            return new TodoServiceModels
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                CompletedAt = todo.CompletedAt,
                Priority = todo.Priority,
                Category = todo.Category,
                DueDate = todo.DueDate,
                UserId = todo.UserId,
                Username = todo.User?.Username ?? string.Empty
            };
        }

        public async Task<IEnumerable<TodoServiceModels>> GetAllTodos()
        {
            var todos = await _todoRepository.GetAllTodos();
            return todos.Select(t => new TodoServiceModels
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt,
                Priority = t.Priority,
                Category = t.Category,
                DueDate = t.DueDate,
                UserId = t.UserId,
                Username = t.User?.Username ?? string.Empty
            });
        }

        public async Task<IEnumerable<TodoServiceModels>> GetTodosByUserId(int userId)
        {
            var todos = await _todoRepository.GetTodosByUserId(userId);
            return todos.Select(t => new TodoServiceModels
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt,
                Priority = t.Priority,
                Category = t.Category,
                DueDate = t.DueDate,
                UserId = t.UserId,
                Username = t.User?.Username ?? string.Empty
            });
        }

        public async Task<TodoServiceModels?> UpdateTodo(int id, string title, string description, int priority, string category, DateTime dueDate, bool isCompleted)
        {
            var existingTodo = await _todoRepository.GetTodoById(id);
            if (existingTodo == null) return null;

            existingTodo.Title = title;
            existingTodo.Description = description;
            existingTodo.Priority = priority;
            existingTodo.Category = category;
            existingTodo.DueDate = dueDate;
            existingTodo.IsCompleted = isCompleted;

            if (isCompleted && existingTodo.CompletedAt == null)
            {
                existingTodo.CompletedAt = DateTime.UtcNow;
            }
            else if (!isCompleted)
            {
                existingTodo.CompletedAt = null;
            }

            var updatedTodo = await _todoRepository.UpdateTodo(existingTodo);
            
            return new TodoServiceModels
            {
                Id = updatedTodo.Id,
                Title = updatedTodo.Title,
                Description = updatedTodo.Description,
                IsCompleted = updatedTodo.IsCompleted,
                CreatedAt = updatedTodo.CreatedAt,
                CompletedAt = updatedTodo.CompletedAt,
                Priority = updatedTodo.Priority,
                Category = updatedTodo.Category,
                DueDate = updatedTodo.DueDate,
                UserId = updatedTodo.UserId,
                Username = updatedTodo.User?.Username ?? string.Empty
            };
        }

        public async Task<bool> DeleteTodo(int id)
        {
            return await _todoRepository.DeleteTodo(id);
        }

        public async Task<bool> MarkTodoAsCompleted(int id)
        {
            var todo = await _todoRepository.GetTodoById(id);
            if (todo == null) return false;

            todo.IsCompleted = true;
            todo.CompletedAt = DateTime.UtcNow;

            await _todoRepository.UpdateTodo(todo);
            return true;
        }

        public async Task<IEnumerable<TodoServiceModels>> GetTodosByCategory(string category)
        {
            var todos = await _todoRepository.GetTodosByCategory(category);
            return todos.Select(t => new TodoServiceModels
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt,
                Priority = t.Priority,
                Category = t.Category,
                DueDate = t.DueDate,
                UserId = t.UserId,
                Username = t.User?.Username ?? string.Empty
            });
        }

        public async Task<IEnumerable<TodoServiceModels>> GetTodosByPriority(int priority)
        {
            var todos = await _todoRepository.GetTodosByPriority(priority);
            return todos.Select(t => new TodoServiceModels
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt,
                Priority = t.Priority,
                Category = t.Category,
                DueDate = t.DueDate,
                UserId = t.UserId,
                Username = t.User?.Username ?? string.Empty
            });
        }
    }
}
