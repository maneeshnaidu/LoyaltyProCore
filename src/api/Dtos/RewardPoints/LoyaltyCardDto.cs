using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.RewardPoints
{
    public class LoyaltyCardDto
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public string VendorLogo { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public decimal MaxPoints { get; set; }
    }
}