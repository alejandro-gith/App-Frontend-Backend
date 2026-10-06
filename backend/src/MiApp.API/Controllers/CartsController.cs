using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;

namespace MiApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

   [HttpPost]
    public async Task<IActionResult> AddToCart(
        [FromBody] AddToCartRequest request)
    {
    try
    {
        var cart = await _cartService.AddToCartAsync(request);
        return Ok(cart);
    }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {   
            return NotFound(new { message = ex.Message });
        }
    }

    // --- ENDPOINTS AÑADIDOS PARA LA US10 ---

    [HttpGet("{userId}")]
    public IActionResult GetCart(int userId)
    {
        var cart = _cartService.GetByUserId(userId);
        if (cart == null)
        {
            return NotFound(new { message = "El usuario no tiene un carrito activo." });
        }
        return Ok(cart);
    }

    [HttpPut("{userId}/items")]
    public IActionResult UpdateItemQuantity(int userId, [FromBody] UpdateCartItemRequest request)
    {
        try
        {
            var cart = _cartService.UpdateItemQuantity(userId, request);
            return Ok(cart);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{userId}/items/{productId}")]
    public IActionResult RemoveItem(int userId, int productId)
    {
        try
        {
            var cart = _cartService.RemoveItem(userId, productId);
            return Ok(cart);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}