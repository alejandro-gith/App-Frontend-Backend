using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public class CartQueryRepository : ICartQueryRepository
{
    public IEnumerable<Cart> GetAll()
    {
        return InMemoryData.Carts.ToList();
    }
}