using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IBoardRepository
    {
        DbSet<Board> Boards {get;}
        Task<Board> GetBoardByIdAsync(Guid boardId);

        void Add(Board board);

        Task<int> SaveChangesAsync();
        void InitializeDb();
    }
}