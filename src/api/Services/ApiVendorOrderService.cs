using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class ApiVendorOrderService : BaseVendorOrderService, IVendorOrderService
    {
        public ApiVendorOrderService(
            VendorIntegration integration,
            IHttpClientFactory httpClientFactory,
            ILogger<ApiVendorOrderService> logger)
            : base(integration, httpClientFactory, logger)
        {
        }

        public async Task<List<VendorOrder>> GetOrdersAsync(int vendorIntegrationId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using var client = CreateHttpClient();
                var queryParams = new Dictionary<string, string>();

                if (startDate.HasValue)
                    queryParams.Add("startDate", startDate.Value.ToString("o"));
                if (endDate.HasValue)
                    queryParams.Add("endDate", endDate.Value.ToString("o"));

                var queryString = new FormUrlEncodedContent(queryParams).ReadAsStringAsync();
                var response = await client.GetAsync($"orders?{queryString}");

                response.EnsureSuccessStatusCode();

                var orders = await response.Content.ReadFromJsonAsync<List<VendorOrder>>();
                return orders ?? new List<VendorOrder>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders from vendor {VendorId}", vendorIntegrationId);
                throw;
            }
        }

        public async Task<VendorOrder?> GetOrderByIdAsync(int vendorIntegrationId, string orderId)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.GetAsync($"orders/{orderId}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<VendorOrder>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order {OrderId} from vendor {VendorId}", orderId, vendorIntegrationId);
                throw;
            }
        }
    }
}