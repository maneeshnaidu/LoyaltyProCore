using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserByUserCodeAsync(int userCode);
        Task<int> GenerateUserCodeAsync();
        Task<int> GenerateAdminUserCodeAsync();
    }
}