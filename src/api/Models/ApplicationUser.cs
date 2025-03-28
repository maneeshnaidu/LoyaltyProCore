using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int? OutletId { get; set; }
        public Outlet? Outlet { get; set; }
        public List<Vendor>? FavoriteVendors { get; set; } = new List<Vendor>();
        public List<Points>? StampCard { get; set; } = new List<Points>();
    }
}