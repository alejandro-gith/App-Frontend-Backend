using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAll();

    User? GetById(int id);

    User? GetByUsername(string username);
}