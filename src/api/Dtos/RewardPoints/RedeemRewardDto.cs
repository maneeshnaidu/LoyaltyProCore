using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.RewardPoints
{
    public class RedeemRewardDto
    {
        public int CustomerCode { get; set; }
        public int RewardId { get; set; }
        public int OutletId { get; set; }
    }
}