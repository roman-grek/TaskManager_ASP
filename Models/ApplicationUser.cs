using System;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }

    public class NewApplicationUser : ApplicationUser
    {
        public string Password { get; set; }
    }

}