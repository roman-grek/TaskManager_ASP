using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface ITaskRepository
    {
        DbSet<Task> Tasks {get;}
        System.Threading.Tasks.Task<Task> GetTaskByIdAsync(Guid taskId);
    }
}