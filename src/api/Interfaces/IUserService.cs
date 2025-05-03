using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser?>> GetAllAsync(QueryObject query);
        Task<ApplicationUser?> GetUserByUserCodeAsync(int userCode);
        Task<ApplicationUser?> GetUserByUserNameAsync(string userName);
        Task<string?> GetUsernameByIdAsync(string id);
        Task<int?> GetVendorByIdAsync(string id);
        Task<int> GenerateUserCodeAsync();
        Task<int> GenerateAdminUserCodeAsync();
    }
}