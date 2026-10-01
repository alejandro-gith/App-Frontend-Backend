using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface ICartRepository
{
    IEnumerable<Cart> GetAll();

    Cart? GetById(int id);

    Cart? GetByUserId(int userId);

    Cart Add(Cart cart);

    bool Update(Cart cart);

    bool Delete(int id);
}