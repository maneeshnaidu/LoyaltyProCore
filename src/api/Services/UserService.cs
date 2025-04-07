
using api.Interfaces;
using api.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApplicationUser?> GetUserByUserCodeAsync(int userCode)
        {
            return await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserCode == userCode);
        }

        public async Task<int> GenerateUserCodeAsync()
        {
            var random = new Random();
            int newCode;

            do
            {
                newCode = random.Next(10000, 99999); // Generate a random 5-digit number
                // Check if the generated code already exists in the database
            }
            while (await _userManager.Users.AnyAsync(u => u.UserCode == newCode));

            return newCode;
        }
    }
}