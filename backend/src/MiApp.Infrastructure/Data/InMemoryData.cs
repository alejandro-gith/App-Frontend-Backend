using MiApp.Domain.Entities;
using MiApp.Domain.Enums;

namespace MiApp.Infrastructure.Data;

public static class InMemoryData
{
    public static List<User> Users { get; } = new()
    {
        new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@miapp.com",
            Password = "123",
            Role = UserRole.Administrator,
            FullName = "Administrador de Prueba",
            Phone = "0000000001",
        },

        new User
        {
            Id = 2,
            Username = "cliente",
            Email = "cliente@miapp.com",
            Password = "123",
            Role = UserRole.Client,
            FullName = "Cliente de Prueba",
            Phone = "0000000002",
        },

        new User
        {
            Id = 3,
            Username = "auditor",
            Email = "auditor@miapp.com",
            Password = "123",
            Role = UserRole.Auditor,
            FullName = "Auditor de Prueba",
            Phone = "0000000003",
        }
    };

    public static List<Product> Products { get; } = new()
    {
        new Product
        {
            Id = 1,
            Title = "Laptop Gamer",
            Price = 1200.00m,
            Category = "Tecnología",
            Description = "Laptop para desarrollo y juegos",
            Image = "https://via.placeholder.com/150"
        },

        new Product
        {
            Id = 2,
            Title = "Smartphone Pro",
            Price = 800.00m,
            Category = "Tecnología",
            Description = "Teléfono con cámara de alta definición",
            Image = "https://via.placeholder.com/150"
        },

        new Product
        {
            Id = 3,
            Title = "Playera de Algodón",
            Price = 25.00m,
            Category = "Ropa",
            Description = "Playera cómoda 100% algodón",
            Image = "https://via.placeholder.com/150"
        }
    };

    public static List<Cart> Carts { get; } = new();
}