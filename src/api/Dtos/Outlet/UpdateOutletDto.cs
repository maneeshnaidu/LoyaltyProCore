using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Outlet
{
    public class UpdateOutletDto
    {
        [Required]
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}