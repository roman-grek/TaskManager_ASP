using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface ITableRepository
    {
        IEnumerable<Table> Tables {get;}
        Table GetTableById(int tableId);
    }
}