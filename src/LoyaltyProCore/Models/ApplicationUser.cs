using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace LoyaltyProCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int? OutletId { get; set; }
        public Outlet? Outlet { get; set; }
        public List<Vendor>? FavoriteVendors { get; set; } = new List<Vendor>();
        public List<Points>? StampCard { get; set; } = new List<Points>();
    }
}