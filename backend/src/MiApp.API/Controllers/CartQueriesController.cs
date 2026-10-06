using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Interfaces;

namespace MiApp.API.Controllers;

[ApiController]
[Route("api/carts")]
public class CartQueriesController : ControllerBase
{
    private readonly ICartQueryService _cartService;

    public CartQueriesController(ICartQueryService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,Auditor")]
    public IActionResult GetAll()
    {
        return Ok(_cartService.GetAll());
    }
}