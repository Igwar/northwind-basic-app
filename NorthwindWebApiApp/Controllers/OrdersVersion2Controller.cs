using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindWebApiApp.Models;
using NorthwindWebApiApp.Services;

namespace NorthwindWebApiApp.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/orders")]
    public class OrdersVersion2Controller : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrdersController> logger;
        public OrdersVersion2Controller(IOrderService orderService, ILogger<OrdersController> logger)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BriefOrderModel>>> GetOrders()
        {
            this.logger.LogInformation("Calling OrdersController.GetOrders");
            try
            {
                return this.Ok(await this.orderService.GetExtendedOrdersAsync());
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception in OrdersController.GetOrders.");
                throw;
            }
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<FullOrderModel>> GetOrder(int orderId)
        {
            this.logger.LogInformation("Calling OrdersController.GetOrders(int orderId)");
            try
            {
                return this.Ok(await this.orderService.GetOrderAsync(orderId));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Exception in OrdersController.GetOrders(int orderId).");
                throw;
            }
        }
    }
}
