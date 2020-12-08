using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.ViewModels;
using TaskManager.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers
{
    [Authorize]
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
    }
}