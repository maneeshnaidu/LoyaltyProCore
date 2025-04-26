
namespace api.Models
{
    public class Outlet
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public required string Address { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? PhoneNumber { get; set; }
        public Vendor? Vendor { get; set; } // Navigation Property
    }
}