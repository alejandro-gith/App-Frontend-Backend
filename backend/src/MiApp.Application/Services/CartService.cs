using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public Cart AddToCart(AddToCartRequest request)
    {
        if (request.Quantity <= 0)
        {
            throw new ArgumentException(
                "La cantidad debe ser mayor a cero."
            );
        }

        var product = _productRepository.GetById(request.ProductId);

        if (product == null)
        {
            throw new KeyNotFoundException(
                "El producto especificado no existe."
            );
        }

        var cart = _cartRepository.GetByUserId(request.UserId);

        if (cart == null)
        {
            cart = new Cart
            {
                Id = new Random().Next(1000, 9999),
                UserId = request.UserId,
                Date = DateTime.Now,
                Products = new List<CartItem>()
            };

            _cartRepository.Add(cart);
        }

        var existingItem = cart.Products
            .FirstOrDefault(
                i => i.ProductId == request.ProductId
            );

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Products.Add(new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
        }

        _cartRepository.Update(cart);

        return cart;
    }

    public Cart? GetByUserId(int userId)
    {
        return _cartRepository.GetByUserId(userId);
    }

    public Cart UpdateItemQuantity(
        int userId,
        UpdateCartItemRequest request)
    {
        if (request.Quantity <= 0)
        {
            throw new ArgumentException(
                "La cantidad debe ser mayor a cero."
            );
        }

        var cart = _cartRepository.GetByUserId(userId);

        if (cart == null)
        {
            throw new KeyNotFoundException(
                "Carrito no encontrado."
            );
        }

        var item = cart.Products
            .FirstOrDefault(
                i => i.ProductId == request.ProductId
            );

        if (item == null)
        {
            throw new KeyNotFoundException(
                "Producto no encontrado en el carrito."
            );
        }

        item.Quantity = request.Quantity;

        _cartRepository.Update(cart);

        return cart;
    }

    public Cart RemoveItem(int userId, int productId)
    {
        var cart = _cartRepository.GetByUserId(userId);

        if (cart == null)
        {
            throw new KeyNotFoundException(
                "Carrito no encontrado."
            );
        }

        var item = cart.Products
            .FirstOrDefault(
                i => i.ProductId == productId
            );

        if (item == null)
        {
            throw new KeyNotFoundException(
                "Producto no encontrado en el carrito."
            );
        }

        cart.Products.Remove(item);

        _cartRepository.Update(cart);

        return cart;
    }
}