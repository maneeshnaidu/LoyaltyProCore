using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Models
{
    public class RewardPoints
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int RewardId { get; set; } = 0;
        public int VendorId { get; set; } = 0;
        public int OutletId { get; set; } = 0;
        public int? OrderId { get; set; }
        public string StaffId { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Points { get; set; } = 0;
        public int Level { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public Vendor? Vendor { get; set; } // Navigation property
        public Reward? Reward { get; set; } // Navigation property
        public Outlet? Outlet { get; set; } // Navigation property
    }
}