using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Data.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public BoardRepository(ApplicationDbContext dbContext)
        {
            _appDbContext = dbContext;
        } 
        public IEnumerable<Board> Boards => _appDbContext.Boards;

        public async Task<Board> GetBoardByIdAsync(Guid boardId) =>
            await _appDbContext.Boards.FirstOrDefaultAsync(b => b.Id == boardId);
        
        public void InitializeDb() => DbInitializer.Seed(_appDbContext);
    }

}