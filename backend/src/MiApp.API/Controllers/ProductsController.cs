using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs.Products;
using MiApp.Application.Interfaces;

namespace MiApp.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetById(id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(CreateProductRequest request)
    {
        var result = _productService.Create(request);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result
        );
    }
}