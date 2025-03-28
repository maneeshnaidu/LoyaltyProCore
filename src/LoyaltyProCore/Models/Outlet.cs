using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Outlet
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Vendor? Vendor { get; set; } // Navigation Property
    }
}