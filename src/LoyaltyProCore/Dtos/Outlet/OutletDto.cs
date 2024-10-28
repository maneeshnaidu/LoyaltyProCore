using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Dtos.Outlet
{
    public class OutletDto
    {
        public int Id { get; set; }        
        public required string Location { get; set; }
        public DateTime CreatedOn { get; set; } 
        public int? VendorId { get; set; }
    }
}