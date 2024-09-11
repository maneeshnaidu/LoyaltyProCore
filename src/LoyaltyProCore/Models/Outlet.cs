using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Outlet
    {
        public int Id { get; set; }
        public int? VendorId { get; set; }
        public required string Location { get; set; }
        public DateTime CreatedOn { get; set; }
        // Navigation Property
        public Vendor? Vendor { get; set; }
    }
}