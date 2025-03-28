using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string ExternalId { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerId { get; set; }
        public int? VendorId { get; set; }
        public int? OutletId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PointsEarned { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Vendor? Vendor { get; set; }
        public Outlet? Outlet { get; set; }
        public ApplicationUser? Customer { get; set; }
    }
}