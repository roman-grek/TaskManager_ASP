using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TaskManager.ViewModels;
using TaskManager.ViewModels.BoardViewModels;

namespace TaskManager.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BoardController> _logger;
        public BoardController(IBoardRepository boardRepository, 
            ITaskRepository taskRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<BoardController> logger)
        {
            _boardRepository = boardRepository;
            _taskRepository = taskRepository;
            _userManager = userManager;
            _logger = logger;
        }
        
        // Get all boards belonging to the currently logged in user
        [HttpGet]
        public IActionResult All()
        {
            var claims = this.User;
            ClaimsPrincipal currentUser = this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var boards = _boardRepository.Boards.Where(board => board.UserId == userId).ToList();
            return View(boards);
        }
        
        // Open a board
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            var t1 = await _taskRepository.Tasks.Where(task => task.BoardId == id.ToString()).Intersect(_taskRepository.Tasks.Where(task => task.ListNum == 1)).ToListAsync();
            var t2 = await _taskRepository.Tasks.Where(task => task.BoardId == id.ToString()).Intersect(_taskRepository.Tasks.Where(task => task.ListNum == 2)).ToListAsync();
            var t3 = await _taskRepository.Tasks.Where(task => task.BoardId == id.ToString()).Intersect(_taskRepository.Tasks.Where(task => task.ListNum == 3)).ToListAsync();

            return View(model: new BoardViewModel { Board = board, TaskL1 = t1, TaskL2 = t2, TaskL3 = t3 });
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            ClaimsPrincipal currentUser = this.User;
            var uId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(new Board { UserId = uId });
        }
        
        // Handle the creation of a new board
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Board board)
        {

            _boardRepository.Add(board);
            await _boardRepository.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
        
        // Return the view to edit the board
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]Guid id)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            return View(board);
        }
        
        // Returns the updated board  and saves the changes
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Board boardUpdate)
        {
            var board = await _boardRepository.GetBoardByIdAsync(boardUpdate.Id);

            board.Title = boardUpdate.Title;
            board.Description = boardUpdate.Description;

            var routeValues = new RouteValueDictionary {
               {"id", board.Id.ToString()}
           };
            _boardRepository.Boards.Update(board);
            await _boardRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Open), "Board", routeValues);
        }
        
        // The delete function for boards
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            var tasks = await _taskRepository.Tasks.Where(task => task.BoardId == id.ToString()).ToListAsync();

            // Remove all tasks associated with the board
            foreach(Models.Task task in tasks)
            {
                _taskRepository.Tasks.Remove(task);
            }

            _boardRepository.Boards.Remove(board);
            await _boardRepository.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        
        // Handles error in production
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}