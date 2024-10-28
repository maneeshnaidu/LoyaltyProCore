using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Dtos.Outlet
{
    public class CreateOutletDto
    {
        public int VendorId { get; set; }
        public required string Location { get; set; }   
    }
}