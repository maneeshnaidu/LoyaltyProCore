using api.Dtos.Outlet;

namespace api.Dtos.Vendor
{
    public class VendorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string LogoImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public required DateTime CreatedOn { get; set; }
        public List<OutletDto>? Outlets { get; set; }
    }
}