using MiApp.Application.DTOs;
using MiApp.Domain.Entities;

namespace MiApp.Application.Interfaces;

public interface ICartService
{
    Cart AddToCart(AddToCartRequest request);
    Cart? GetByUserId(int userId);
    Cart UpdateItemQuantity(int userId, UpdateCartItemRequest request);
    Cart RemoveItem(int userId, int productId);
}