using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.RewardPoints
{
    public class UpsertPointsDto
    {
        public string CustomerId { get; set; } = string.Empty;
        public int RewardId { get; set; }
        public int VendorId { get; set; }
        public int? OutletId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Point { get; set; }
        public int Level { get; set; }
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    }
}