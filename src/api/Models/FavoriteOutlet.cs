
namespace api.Models
{
    public class FavoriteOutlet
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int OutletId { get; set; }
    }
}