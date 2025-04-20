using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class FavoriteVendor
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int VendorId { get; set; }
    }
}