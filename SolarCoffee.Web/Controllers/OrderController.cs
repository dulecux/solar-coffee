using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(ILogger<OrderController> logger, 
        IOrderService orderService, 
        ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice) {

                _logger.LogInformation("Generating invoice");
                
                var order = OrderMapper.SerializeInvoiceToOrder(invoice);
                order.Customer = _customerService.GetCustomerById(invoice.CustomerId);

                _orderService.GenerateOpenOrder(order);

                return Ok();
        }

        [HttpGet]
        public ActionResult GetOrders() {

            var orders = _orderService.GetOrders();
            var orderModels = OrderMapper.SerializeOrdersToViewModels(orders);

            return Ok(orderModels);
        }

         [HttpPatch("/complete/{id}")]
        public ActionResult MarkOrderComplete(int id) {

            _logger.LogInformation($"Marking order {id} complete...");
            _orderService.MarkFulfilled(id);

            return Ok();
        }


        
    }
}