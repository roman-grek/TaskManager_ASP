using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IBoardRepository
    {
        IEnumerable<Board> Boards {get;}
        Board GetBoardByIdAsync(int boardId);
        void InitializeDb();
    }
}