using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Vendor
{
    public class UpdateVendorRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Company Name cannot be over 10 over characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "Description cannot be over 10 over characters")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "Category cannot be over 10 over characters")]
        public string Category { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}