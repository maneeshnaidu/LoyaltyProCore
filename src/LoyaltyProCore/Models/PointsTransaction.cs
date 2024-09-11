using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class PointsTransaction
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderId { get; set; }
        public decimal Points { get; set; }
        public required string TransactionType { get; set; }
        public int? OutletId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}