using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Device
{
    public class UpdateDeviceDto
    {
        public required string DeviceId { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string DeviceToken { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}