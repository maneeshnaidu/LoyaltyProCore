using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Points
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VendorId { get; set; }
        public int? OutletId { get; set; }
        public decimal Point { get; set; }
        public int Level { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}