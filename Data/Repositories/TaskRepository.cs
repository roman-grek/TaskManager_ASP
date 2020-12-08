using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private ApplicationDbContext _appDbContext;

        public TaskRepository(ApplicationDbContext dbContext)
        {
            _appDbContext = dbContext;
        } 
        public DbSet<Task> Tasks => _appDbContext.Tasks;

        public async System.Threading.Tasks.Task<Task> GetTaskByIdAsync(Guid taskId) =>
            await _appDbContext.Tasks.FindAsync(taskId);
    }
}