using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Device
    {
        public int Id { get; set; }
        public required string DeviceId { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceToken { get; set; } = string.Empty;
        public string VendorId { get; set; } = string.Empty;
        public Vendor? Vendor { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}