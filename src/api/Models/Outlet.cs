
namespace api.Models
{
    public class Outlet
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? PhoneNumber { get; set; }
        public Vendor? Vendor { get; set; } // Navigation Property
    }
}