using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("/api/inventories")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public ActionResult GetCurrentInventory() {
            _logger.LogInformation("Getting all inventory...");

            var inventory = _inventoryService.GetCurrentInventory()
                            .Select(pi => new ProductInventoryModel{
                                Id = pi.Id,
                                QuantityOnHand = pi.QuantityOnHand,
                                IdealQuantity = pi.IdealQuantity,
                                Product = ProductMapper.SerializeProductModel(pi.Product)
                            })
                            .OrderBy(inv => inv.Product.Name)
                            .ToList();


            return Ok(inventory);
        }

         [HttpPatch]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment) {
            _logger.LogInformation($"Updating inventory for {shipment.ProductId} - Adjustment: {shipment.Adjustment}");

            var id = shipment.ProductId;
            var adjustment = shipment.Adjustment;

            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);

            return Ok(inventory);
        }


        
    }
}