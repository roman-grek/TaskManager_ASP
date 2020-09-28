using System;

namespace TaskManager.Models
{
    public class Card
    {
        public int CardId {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public int TableId {get; set;}
        public virtual Table Table {get; set;}
    }
}