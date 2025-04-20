using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.RewardPoints;
using api.Dtos.Vendor;

namespace api.Dtos.User
{
    public class UserDto
    {
        public required string Id { get; set; }
        public int UserCode { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? VendorId { get; set; }
        public int? OutletId { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<string>? Roles { get; set; } = [];
        public List<PointsDto>? StampCard { get; set; } = [];
    }
}