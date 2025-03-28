using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Dtos.Vendor
{
    public class CreateVendorRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}