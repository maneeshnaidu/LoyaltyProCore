
namespace api.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public required string AdminId { get; set; }
        public bool IsActive { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public List<Outlet> Outlets { get; set; } = new List<Outlet>();
        public List<Reward> RewardPrograms { get; set; } = new List<Reward>();
        public ApplicationUser? Admin { get; set; }
    }
}