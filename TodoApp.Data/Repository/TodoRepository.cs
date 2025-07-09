using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data.Entities;
using TodoApp.Data.IRepository;

namespace TodoApp.Data.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> CreateTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return await GetTodoById(todo.Id) ?? todo;
        }

        public async Task<Todo?> GetTodoById(int id)
        {
            return await _context.Todos
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _context.Todos
                .Include(t => t.User)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetTodosByUserId(int userId)
        {
            return await _context.Todos
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<Todo> UpdateTodo(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return await GetTodoById(todo.Id) ?? todo;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return false;

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Todo>> GetTodosByCategory(string category)
        {
            return await _context.Todos
                .Include(t => t.User)
                .Where(t => t.Category == category)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetTodosByPriority(int priority)
        {
            return await _context.Todos
                .Include(t => t.User)
                .Where(t => t.Priority == priority)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Todo>> SearchTodos(string query)
        {
            return await _context.Todos
                .Include(t => t.User)
                .Where(t => t.Title.Contains(query) || t.Description.Contains(query))
                .OrderBy(t => t.DueDate)
                .ToListAsync();


        }
    }
}
