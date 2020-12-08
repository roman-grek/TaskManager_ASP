using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.ViewModels.BoardViewModels
{
    public class BoardViewModel
    {
        public Board Board {get; set;}
        
        public IEnumerable<Task> Tasks { get; set; }
        
        public IEnumerable<Task> TaskL1 {get; set;}
        
        public IEnumerable<Task> TaskL2 {get; set;}
        
        public IEnumerable<Task> TaskL3 {get; set;}
    }
}