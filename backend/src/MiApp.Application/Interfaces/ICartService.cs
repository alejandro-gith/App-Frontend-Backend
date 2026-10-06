using MiApp.Application.DTOs;
using MiApp.Domain.Entities;

namespace MiApp.Application.Interfaces;

public interface ICartService
{
    Task<Cart> AddToCartAsync(AddToCartRequest request);

    Cart? GetByUserId(int userId);

    Cart UpdateItemQuantity(
        int userId,
        UpdateCartItemRequest request
    );

    Cart RemoveItem(
        int userId,
        int productId
    );
}