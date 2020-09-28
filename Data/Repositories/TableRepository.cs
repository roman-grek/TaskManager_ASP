using System;
using System.Collections.Generic;
using System.Linq;

using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Data.Repositories
{
    public class TableRepository : ITableRepository
    {
        private ApplicationDbContext appDbContext;

        public TableRepository(ApplicationDbContext dbContext)
        {
            appDbContext = dbContext;
        } 
        public IEnumerable<Table> Tables => appDbContext.Tables;

        public Table GetTableById(int tableId) =>
            appDbContext.Tables.FirstOrDefault(p => p.TableId == tableId);
    }
}