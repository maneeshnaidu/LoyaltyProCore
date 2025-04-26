using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserCode { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int? VendorId { get; set; }
        public int? OutletId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Outlet? Outlet { get; set; }
        public List<RewardPoints>? StampCard { get; set; } = [];
        public Vendor? Vendor { get; set; }
    }
}