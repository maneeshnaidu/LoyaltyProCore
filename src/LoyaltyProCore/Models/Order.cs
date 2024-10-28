using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string CustomerId { get; set; }
        public int OutletId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PointsEarned { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}