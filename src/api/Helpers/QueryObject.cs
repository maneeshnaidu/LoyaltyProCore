using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? CustomerId { get; set; }
        public string? Role { get; set; } = null;
        public int? UserCode { get; set; } = null;
        public int? VendorId { get; set; } = null;
        public int? OutletId { get; set; } = null;
        public string? Category { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Address { get; set; }
        public bool? IsLatest { get; set; } = false;
        public string? SortBy { get; set; } = null;
        public DateTime? CreatedDate { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}