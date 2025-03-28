using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoyaltyProCore.Dtos.Reward
{
    public class CreateRewardDto
    {
        public int VendorId { get; set; }
        [Required]
        public required string Title { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PointsRequired { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}