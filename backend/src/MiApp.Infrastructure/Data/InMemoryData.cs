using MiApp.Domain.Entities;
using System.Collections.Generic;

namespace MiApp.Infrastructure.Data
{
    public static class InMemoryData
    {
        // Se respeta la lista de usuarios para no romper el Login
        public static List<User> Users { get; set; } = new List<User>();

        public static List<Cart> Carts { get; set; } = new List<Cart>();

        // Lista de productos para US03, US04 y US05
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Pro", Description = "Laptop de alto rendimiento.", Price = 1500.00m, Category = "Computacion", ImageUrl = "https://via.placeholder.com/150", Stock = 10 },
            new Product { Id = 2, Name = "Teclado Mecánico", Description = "Teclado con switches red.", Price = 100.00m, Category = "Accesorios", ImageUrl = "https://via.placeholder.com/150", Stock = 25 },
            new Product { Id = 3, Name = "Monitor 4K", Description = "Monitor IPS 27 pulgadas.", Price = 350.00m, Category = "Computacion", ImageUrl = "https://via.placeholder.com/150", Stock = 5 }
        };
    }
}