using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Dtos.Device
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public required string DeviceId { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceToken { get; set; } = string.Empty;
        public int VendorId { get; set; }
        public bool IsActive { get; set; }
    }
}