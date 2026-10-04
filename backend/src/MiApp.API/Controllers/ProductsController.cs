using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs; // US06: necesario para reconocer CreateProductRequest
using MiApp.Application.Interfaces;
using System.Threading.Tasks;

namespace MiApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetCatalogAsync();
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var result = await _productService.GetCatalogByCategoryAsync(category);
            if (!result.IsSuccess) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            var result = await _productService.GetProductDetailAsync(id);
            if (!result.IsSuccess) return NotFound(result.Error);
            return Ok(result.Value);
        }

        // US06: POST api/products. Recibe los datos del producto nuevo en el cuerpo (JSON).
        // El controller solo maneja HTTP: pasa los datos al servicio y traduce su resultado a un código HTTP.
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            // La validación y la lógica de negocio viven en el servicio, no aquí
            var result = await _productService.CreateProductAsync(request);

            // Si el servicio rechazó los datos, responde 400 con el motivo
            if (!result.IsSuccess) return BadRequest(result.Error);

            // Si todo salió bien, responde 201 Created con el producto nuevo y su ID,
            // e indica en la cabecera Location dónde consultarlo (el endpoint de detalle)
            return CreatedAtAction(nameof(GetProductDetail), new { id = result.Value!.Id }, result.Value);
        }
    }
}