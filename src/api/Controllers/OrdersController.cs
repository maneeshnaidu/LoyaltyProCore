
using api.Helpers;
using api.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/order/{vendorId}")]
    public class OrdersController : ControllerBase
    {
        private readonly IVendorOrderServiceFactory _serviceFactory;
        private readonly IIntegrationRepository _integrationRepository;

        public OrdersController(
            IVendorOrderServiceFactory serviceFactory,
            IIntegrationRepository integrationRepository
            )
        {
            _serviceFactory = serviceFactory;
            _integrationRepository = integrationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (query.VendorId != null && !string.IsNullOrEmpty(query.Category))
            {
                // Get the integration for this vendor
                var integration = await _integrationRepository.GetByIdAsync((int)query.VendorId, query.Category);

                if (integration == null)
                    return NotFound("Order integration not configured for this vendor");

                if (!integration.IsActive)
                    return BadRequest("Order integration is not active");

                var service = _serviceFactory.Create(integration);
                var orders = await service.GetOrdersAsync(integration.Id, query.StartDate, query.EndDate);

                return Ok(orders);
            }
            else
            {
                return NotFound("Vendor or integration category does not exist.");
            }
        }
    }
}