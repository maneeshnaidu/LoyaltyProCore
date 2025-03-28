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
        public DateTime RedeemedOn { get; set; } = DateTime.Now;
    }
}