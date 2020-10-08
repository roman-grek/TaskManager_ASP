using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using TaskManager.Models;
using TaskManager.ViewModels;
using TaskManager.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        public BoardController(IBoardRepository boardRepository,
        UserManager<ApplicationUser> userManager,
        ILogger<AccountController> logger) {
            _boardRepository = boardRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public ViewResult Index() {
            //_boardRepository.InitializeDb();
            var vm = new BoardListViewModel();
            var userId = _userManager.GetUserId(HttpContext.User);
            vm.Boards = _boardRepository.Boards.Where(p => p.Owner == userId);
            _logger.LogInformation(1, "User boards are shown");
            return View(vm);
        }

        [HttpGet("/{id}")]
        public ViewResult Details(int id)
        {
            var board = _boardRepository.GetBoardByIdAsync(id);
            return View(board);
        } 
    }
}