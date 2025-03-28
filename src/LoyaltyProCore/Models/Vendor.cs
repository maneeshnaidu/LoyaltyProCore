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
        public string Category { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<Outlet> Outlets { get; set; } = new List<Outlet>();
        public List<Reward> RewardPrograms { get; set; } = new List<Reward>();
    }
}