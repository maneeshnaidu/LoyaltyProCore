using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Interfaces
{
    public interface IVendorOrderService
    {
        Task<List<VendorOrder>> GetOrdersAsync(int vendorIntegrationId, DateTime? startDate, DateTime? endDate);
        Task<VendorOrder?> GetOrderByIdAsync(int vendorIntegrationId, string orderId);
    }
}