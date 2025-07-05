using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data.Entities;

namespace TodoApp.Data
{
    public class AppDbContext : DbContext
    {
       

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>().ToTable("Todos");
            modelBuilder.Entity<User>().ToTable("Users");
            // Configure relationships, indexes, etc. if needed
            modelBuilder.Entity<User>()
                .HasMany(u => u.Todos)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}
