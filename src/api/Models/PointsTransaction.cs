using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PointsTransaction
    {
        public int Id { get; set; }
        public required string CustomerId { get; set; }
        public int? OrderId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Points { get; set; }
        public required string TransactionType { get; set; }
        public int? OutletId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}