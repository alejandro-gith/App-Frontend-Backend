using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    public IEnumerable<Cart> GetAll()
    {
        return InMemoryData.Carts;
    }

    public Cart? GetById(int id)
    {
        return InMemoryData.Carts
            .FirstOrDefault(c => c.Id == id);
    }

    public Cart? GetByUserId(int userId)
    {
        return InMemoryData.Carts
            .FirstOrDefault(c => c.UserId == userId);
    }

    public Cart Add(Cart cart)
    {
        InMemoryData.Carts.Add(cart);
        return cart;
    }

    public bool Update(Cart cart)
    {
        var index = InMemoryData.Carts
            .FindIndex(c => c.Id == cart.Id);

        if (index == -1)
        {
            return false;
        }

        InMemoryData.Carts[index] = cart;
        return true;
    }

    public bool Delete(int id)
    {
        var cart = InMemoryData.Carts
            .FirstOrDefault(c => c.Id == id);

        if (cart == null)
        {
            return false;
        }

        InMemoryData.Carts.Remove(cart);
        return true;
    }
}