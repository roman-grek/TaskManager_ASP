using System;
using System.Collections.Generic;
using System.Linq;

using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Data.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private ApplicationDbContext appDbContext;

        public BoardRepository(ApplicationDbContext dbContext)
        {
            this.appDbContext = dbContext;
        } 
        public IEnumerable<Board> Boards => appDbContext.Boards;

        public Board GetBoardByIdAsync(int boardId) =>
            appDbContext.Boards.FirstOrDefault(p => p.BoardId == boardId);
        
        public void InitializeDb() => DbInitializer.Seed(appDbContext);
    }

}