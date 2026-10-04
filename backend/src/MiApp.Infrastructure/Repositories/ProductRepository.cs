using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsync()
        {
            // Copia de la lista para no exponer la lista real de InMemoryData
            return Task.FromResult<IEnumerable<Product>>(InMemoryData.Products.ToList());
        }

        public Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            var products = InMemoryData.Products
                .Where(p => p.Category.Equals(category, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var product = InMemoryData.Products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

                // US06: guarda un producto nuevo en la lista en memoria (InMemoryData).
        // Aquí se genera el ID porque el repositorio es quien maneja el almacenamiento.
        public Task<Product> AddAsync(Product product)
        {
            // ID nuevo = el mayor ID existente + 1 (o 1 si la lista está vacía)
            product.Id = InMemoryData.Products.Any()
                ? InMemoryData.Products.Max(p => p.Id) + 1
                : 1;

            // Agrega el producto a la lista que hace de "base de datos"
            InMemoryData.Products.Add(product);

            // Devuelve el producto con su ID para que el servicio lo use en la respuesta
            return Task.FromResult(product);
        }


                // US07: reemplaza en la lista en memoria el producto que tenga el mismo Id.
        // Devuelve false si no existe ninguno con ese Id (el servicio lo traduce a "no encontrado").
        public Task<bool> UpdateAsync(Product product)
        {
            // Busca la posición del producto dentro de la lista
            var index = InMemoryData.Products.FindIndex(p => p.Id == product.Id);
            if (index < 0) return Task.FromResult(false);

            // Reemplaza el producto viejo por el actualizado
            InMemoryData.Products[index] = product;
            return Task.FromResult(true);
        }
}
}