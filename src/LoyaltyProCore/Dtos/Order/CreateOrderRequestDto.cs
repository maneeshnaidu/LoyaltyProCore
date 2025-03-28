using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Dtos.Order
{
    public class CreateOrderRequestDto
    {
        public required string ExternalId { get; set; }
        public required string CustomerId { get; set; }
        public int OutletId { get; set; }
        public decimal Amount { get; set; }
    }
}