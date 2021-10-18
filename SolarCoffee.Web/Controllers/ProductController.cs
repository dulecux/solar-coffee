using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;
using System.Linq;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult GetProducts() {
            _logger.LogInformation("Getting all products");

            var products = _productService.GetAllProducts();

            var productViewModels = products
                            .Select(product => ProductMapper.SerializeProductModel(product));

            return Ok(productViewModels);
        }
    }
}