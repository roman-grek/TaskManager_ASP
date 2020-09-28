using System;
using System.Collections.Generic;

namespace TaskManager.Models
{
    public class Table
    {
        public int TableId {get; set;}
        public string Title {get; set;}
        public DateTime Created {get; set;}
        public DateTime Updated {get; set;}
        public int BoardId {get; set;}
        public virtual Board Board {get; set;}
        public List<Card> Cards {get; set;}
    }
}