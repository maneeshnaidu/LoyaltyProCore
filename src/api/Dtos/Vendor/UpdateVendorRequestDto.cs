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
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}