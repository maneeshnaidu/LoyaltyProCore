using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Vendor
{
    public class CreateVendorRequestDto
    {
        [Required]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public IFormFile? CoverImage { get; set; }
        public string? CoverImageUrl { get; set; }
        public IFormFile? LogoImage { get; set; }
        public string? LogoImageUrl { get; set; }
    }
}