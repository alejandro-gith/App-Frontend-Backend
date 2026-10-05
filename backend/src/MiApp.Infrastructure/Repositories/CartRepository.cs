using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    public Cart? GetByUserId(int userId)
    {
        return InMemoryData.Carts.FirstOrDefault(c => c.UserId == userId);
    }

    public void Add(Cart cart)
    {
        InMemoryData.Carts.Add(cart);
    }

    public void Update(Cart cart)
    {
        var index = InMemoryData.Carts.FindIndex(c => c.Id == cart.Id);
        if (index != -1)
        {
            InMemoryData.Carts[index] = cart;
        }
    }
}