using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.ViewModels.TaskViewModels;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TaskController> _logger;
        
        // Initialize the private value _context
        public TaskController(IBoardRepository boardRepository, 
            ITaskRepository taskRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<TaskController> logger)
        {
            _boardRepository = boardRepository;
            _taskRepository = taskRepository;
            _userManager = userManager;
            _logger = logger;
        }
        
        // Open a task
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute]Guid id)
        {

            var task = await _taskRepository.Tasks.FindAsync(id);
            var boardId = Guid.Parse(task.BoardId);
            var board = await _boardRepository.GetBoardByIdAsync(boardId);
            return View(model: new TaskDetailsViewModel { Task = task, Board = board });
        }
        
        // Serve the add task page
        [HttpGet]
        public IActionResult Add([FromRoute]Guid id)
        {
            return View(new Models.Task { BoardId = id.ToString() });
        }
        
        // Handle the creation of a new task
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Models.Task task)
        {
            var routeValues = new RouteValueDictionary {
               {"id", task.BoardId}
           };
            _taskRepository.Tasks.Add(new Models.Task { Title = task.Title, Description = task.Description, BoardId = task.BoardId.ToString(), ListNum = task.ListNum });
            await _boardRepository.SaveChangesAsync();
            return RedirectToAction("Open", "Board", routeValues);
        }
        
        // Return the view to edit the task
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return View(task);
        }
        
        // Returns the updated task and saves the changes
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Models.Task taskUpdate)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskUpdate.Id);

            task.Title = taskUpdate.Title;
            task.Description = taskUpdate.Description;
            task.DueDate = taskUpdate.DueDate;
            task.ListNum = taskUpdate.ListNum;


            var routeValues = new RouteValueDictionary 
            {
               {"id", task.Id.ToString()}
            };
            _taskRepository.Tasks.Update(task);
            await _boardRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Open), "Task", routeValues);
        }
        
        // The delete function for tasks
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);

            var routeValues = new RouteValueDictionary 
            { 
                {"id", task.BoardId}
            };

            _taskRepository.Tasks.Remove(task);
            await _boardRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Open), "Board", routeValues);
        }

}