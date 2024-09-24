using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class CustomerRewards
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? RewardId { get; set; }
        public DateTime RedeemedOn { get; set; } = DateTime.Now;
    }
}