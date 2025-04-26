using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Models;

namespace api.Services
{
    public class BaseVendorOrderService
    {
        protected readonly VendorIntegration _integration;
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ILogger<BaseVendorOrderService> _logger;

        protected BaseVendorOrderService(
            VendorIntegration integration,
            IHttpClientFactory httpClientFactory,
            ILogger<BaseVendorOrderService> logger)
        {
            _integration = integration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected virtual HttpClient CreateHttpClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_integration.ApiUrl);

            // Add authentication based on the integration's auth method
            switch (_integration.AuthMethod)
            {
                case "ApiKey":
                    client.DefaultRequestHeaders.Add("X-API-KEY", _integration.ApiKey);
                    break;
                case "OAuth2":
                    // You would implement OAuth token acquisition here
                    break;
                case "ClientCertificate":
                    // Implement certificate authentication
                    break;
            }

            return client;
        }
    }
}