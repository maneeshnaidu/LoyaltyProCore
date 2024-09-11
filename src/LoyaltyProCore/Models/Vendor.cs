using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required DateTime CreatedOn { get; set; }
        public List<Outlet> Outlets { get; set; } = new List<Outlet>();
    }
}