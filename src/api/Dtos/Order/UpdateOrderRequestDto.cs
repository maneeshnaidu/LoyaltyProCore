using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Order
{
    public class UpdateOrderRequestDto
    {
        public required string ExternalId { get; set; }
        public string? CustomerId { get; set; }
        public int OutletId { get; set; }
        public decimal Amount { get; set; }
        public decimal PointsEarned { get; set; }
    }
}