using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Reward
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public required string Title { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PointsRequired { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Vendor? Vendor { get; set; }
    }
}