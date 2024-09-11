using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? OutletId { get; set; }
        public decimal Amount { get; set; }
        public decimal PointsEarned { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}