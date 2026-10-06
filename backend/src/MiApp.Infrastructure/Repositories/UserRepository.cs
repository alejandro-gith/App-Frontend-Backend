using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public IEnumerable<User> GetAll()
    {
        return InMemoryData.Users;
    }

    public User? GetById(int id)
    {
        return InMemoryData.Users
            .FirstOrDefault(user => user.Id == id);
    }

    public User? GetByUsername(string username)
    {
        return InMemoryData.Users
            .FirstOrDefault(user =>
                user.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase
                )
            );
    }
}