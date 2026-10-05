using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public Cart AddToCart(AddToCartRequest request)
    {
        // Validaciones del Backend
        if (request.Quantity <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor a cero.");
        }

        var product = _productRepository.GetById(request.ProductId);
        if (product == null)
        {
            throw new KeyNotFoundException("El producto especificado no existe.");
        }

        // Obtener o crear carrito para el cliente
        var cart = _cartRepository.GetByUserId(request.UserId);
        if (cart == null)
        {
            cart = new Cart
            {
                Id = new Random().Next(1000, 9999),
                UserId = request.UserId,
                Items = new List<CartItem>()
            };
            _cartRepository.Add(cart);
        }

        // Regla US09: Si el producto ya existe en el carrito, sumar cantidad
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
        }

        _cartRepository.Update(cart);
        return cart;
    }
}