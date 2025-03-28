using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Reward
{
    public class RewardDto
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public required string Title { get; set; }
        public decimal PointsRequired { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}