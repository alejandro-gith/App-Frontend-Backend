using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface ICartQueryRepository
{
    IEnumerable<Cart> GetAll();
}