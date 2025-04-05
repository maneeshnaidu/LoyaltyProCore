using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class CustomerRewards
    {
        public int Id { get; set; }
        public required string CustomerId { get; set; }
        public int RewardId { get; set; }
        public int VendorId { get; set; }
        public int? OutletId { get; set; }
        public string? ExternalId { get; set; } // External ID for the reward
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? RedeemedOn { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(180); // Default to 180 days from now

    }
}