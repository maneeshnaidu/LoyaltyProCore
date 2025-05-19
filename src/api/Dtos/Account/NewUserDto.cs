using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserCode { get; set; }
        public string Email { get; set; } = string.Empty;
        public int? Vendor { get; set; }
        public required string Token { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public IList<string?> Roles { get; set; } = [];
    }
}