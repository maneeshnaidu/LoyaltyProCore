using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Models;

namespace LoyaltyProCore.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}