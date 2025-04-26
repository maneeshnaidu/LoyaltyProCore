using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Points { get; set; }
        public required string TransactionType { get; set; }
        public string OutletAddress { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}