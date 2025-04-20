using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class CreateUserDto
    {
        public required string Admin { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public required string Password { get; set; }
    }
}