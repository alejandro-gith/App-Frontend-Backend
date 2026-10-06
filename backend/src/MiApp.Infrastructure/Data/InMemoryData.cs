using MiApp.Domain.Entities;
using MiApp.Domain.Enums;
using System.Collections.Generic;

namespace MiApp.Infrastructure.Data
{
    public static class InMemoryData
    {
        // Usuarios - US01 y US11
        public static List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@miapp.com",
                Password = "123",
                Role = UserRole.Administrator,
                FullName = "Administrador de Prueba",
                Phone = "0000000001"
            },

            new User
            {
                Id = 2,
                Username = "cliente",
                Email = "cliente@miapp.com",
                Password = "123",
                Role = UserRole.Client,
                FullName = "Cliente de Prueba",
                Phone = "0000000002"
            },

            new User
            {
                Id = 3,
                Username = "auditor",
                Email = "auditor@miapp.com",
                Password = "123",
                Role = UserRole.Auditor,
                FullName = "Auditor de Prueba",
                Phone = "0000000003"
            }
        };

        // Carritos - US09 y US10
        public static List<Cart> Carts { get; set; } = new List<Cart>();

        // Productos - US03, US04, US05, US06, US07 y US08
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop Pro",
                Description = "Laptop de alto rendimiento.",
                Price = 1500.00m,
                Category = "Computacion",
                ImageUrl = "https://picsum.photos/id/0/300/200",
                Stock = 10
            },

            new Product
            {
                Id = 2,
                Name = "Teclado Mecánico",
                Description = "Teclado con switches red.",
                Price = 100.00m,
                Category = "Accesorios",
                ImageUrl = "https://picsum.photos/id/0/300/200",
                Stock = 25
            },

            new Product
            {
                Id = 3,
                Name = "Monitor 4K",
                Description = "Monitor IPS 27 pulgadas.",
                Price = 350.00m,
                Category = "Computacion",
                ImageUrl = "https://picsum.photos/id/0/300/200",
                Stock = 5
            }
        };
    }
}