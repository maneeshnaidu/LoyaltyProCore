using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Models;
using api.Services;

namespace api.Interfaces
{
    public interface IVendorOrderServiceFactory
    {
        IVendorOrderService Create(VendorIntegration integration);
    }

    public class VendorOrderServiceFactory : IVendorOrderServiceFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerFactory _loggerFactory;

        public VendorOrderServiceFactory(
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory;
            _loggerFactory = loggerFactory;
        }

        public IVendorOrderService Create(VendorIntegration integration)
        {
            return integration.IntegrationType switch
            {
                "API" => new ApiVendorOrderService(
                    integration,
                    _httpClientFactory,
                    _loggerFactory.CreateLogger<ApiVendorOrderService>()),

                "SFTP" => throw new NotImplementedException("SFTP integration not implemented"),

                _ => throw new NotSupportedException(
                    $"Integration type {integration.IntegrationType} is not supported")
            };
        }
    }
}