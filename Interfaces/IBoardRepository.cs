using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IBoardRepository
    {
        IEnumerable<Board> Boards {get;}
        Task<Board> GetBoardByIdAsync(Guid boardId);
        void InitializeDb();
    }
}