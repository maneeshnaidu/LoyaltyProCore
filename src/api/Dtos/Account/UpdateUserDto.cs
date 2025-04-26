using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class UpdateUserDto
    {
        [Required]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public int? VendorId { get; set; }
        public string Roles { get; set; } = string.Empty;

    }
}