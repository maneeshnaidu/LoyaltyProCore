using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProCore.Helpers
{
    public class QueryObject
    {
        public string? Category { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Address { get; set; }
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public DateTime? CreatedDate { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}