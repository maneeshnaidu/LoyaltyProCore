using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Dtos.Outlet
{
    public class UpdateOutletDto
    {
        [Required]
        public string Location { get; set; } = string.Empty;
    }
}