using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace LoyaltyProCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Optional: VendorId for users like company admins or outlet admins
        public int? VendorId { get; set; }

        // Optional: OutletId for users like outlet admins or staff of outlet
        public int? OutletId { get; set; }
        // Optional: PointsId for customer rewards
        public List<Points> Points { get; set; } = new List<Points>();
    }
}