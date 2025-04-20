

using api.Data;
using api.Helpers;
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

        public async Task<ApplicationUser?> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<int?> GetVendorByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user?.VendorId;
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

        public async Task<int> GenerateAdminUserCodeAsync()
        {
            var random = new Random();
            int newCode;

            do
            {
                newCode = random.Next(1000, 9999); // Generate a random 4-digit number
                // Check if the generated code already exists in the database
            }
            while (await _userManager.Users.AnyAsync(u => u.UserCode == newCode));

            return newCode;
        }

        public async Task<List<ApplicationUser>> GetAllAsync(QueryObject query)
        {
            var usersQuery = _userManager.Users.AsQueryable();

            // Filter by Role if provided
            if (!string.IsNullOrEmpty(query.Role))
            {
                // Get users in the specified role
                var usersInRole = await _userManager.GetUsersInRoleAsync(query.Role);
                usersQuery = usersQuery.Where(u => usersInRole.Select(r => r.Id).Contains(u.Id));
            }

            // Filter by Vendor if provided
            if (query.VendorId != null)
            {
                usersQuery = usersQuery.Where(u => u.VendorId == query.VendorId);
            }

            // Execute the query and return the results
            return await usersQuery.ToListAsync();
        }
    }
}