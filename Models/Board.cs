using System;
using System.Collections.Generic;

namespace TaskManager.Models
{
    public class Board
    {
        public int BoardId {get; set;}
        public string Title {get; set;}
        public List<Table> Tables {get; set;}
    }
}