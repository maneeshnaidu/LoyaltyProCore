using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Outlet;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Dtos
{
    public class VendorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public required DateTime CreatedOn { get; set; }
        public List<OutletDto>? Outlets { get; set; }
    }
}