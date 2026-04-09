using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.Core.DTOs;
using MiniEcommerce.Core.ServicesAbstraction;

namespace MiniEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
            => Ok(await _productService.GetProductsAsync(pageIndex, pageSize));

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
            => Ok(await _productService.CreateProductAsync(dto));
    }
}
