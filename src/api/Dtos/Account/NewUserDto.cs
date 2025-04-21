using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDto
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string UserName { get; set; }
        public int UserCode { get; set; }
        public required string Email { get; set; }
        public int? Vendor { get; set; }
        public required string Token { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public IList<string?> Roles { get; set; } = [];
    }
}