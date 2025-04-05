using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Models
{
    public class Points
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int VendorId { get; set; }
        public int? OutletId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Point { get; set; }
        public int Level { get; set; }
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public Vendor? Vendor { get; set; } // Navigation property
        public Outlet? Outlet { get; set; } // Navigation property
        public ApplicationUser? Customer { get; set; } // Navigation property
    }
}