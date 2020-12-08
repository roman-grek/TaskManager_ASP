using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        
        // Initialize the needed private variables
        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        
        // Get the view for adding a user
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        // Adds a User
        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] NewApplicationUser newAppUser)
        {

            var appUser = new ApplicationUser()
            {
                UserName = newAppUser.UserName,
                Email = newAppUser.Email
            };
            await _userManager.CreateAsync(appUser, newAppUser.Password);
            
            return RedirectToAction("Login","Account");
        }
}