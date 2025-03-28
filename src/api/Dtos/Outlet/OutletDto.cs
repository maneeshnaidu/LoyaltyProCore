using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Outlet
{
    public class OutletDto
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; }
        public int? VendorId { get; set; }
    }
}