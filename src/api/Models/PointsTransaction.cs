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
        public string StaffId { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public int? OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Points { get; set; }
        public required string TransactionType { get; set; }
        public int OutletId { get; set; }
        public string OutletAddress { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}