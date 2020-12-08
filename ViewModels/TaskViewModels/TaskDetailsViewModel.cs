using TaskManager.Models;

namespace TaskManager.ViewModels.TaskViewModels
{
    public class TaskDetailsViewModel
    {
        public Board Board { get; set; }
        
        public Task Task { get; set; }
    }
}