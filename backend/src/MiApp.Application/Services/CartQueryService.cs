using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Services;

public class CartQueryService : ICartQueryService
{
    private readonly ICartQueryRepository _cartRepository;

    public CartQueryService(ICartQueryRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public IEnumerable<CartResponse> GetAll()
    {
        return _cartRepository.GetAll()
            .Select(cart => new CartResponse
            {
                Id = cart.Id,
                Date = cart.Date,
                UserId = cart.UserId,
                Products = cart.Products
                    .Select(item => new CartItemResponse
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    })
                    .ToList()
            })
            .ToList();
    }
}