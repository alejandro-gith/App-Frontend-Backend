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
            return Task.FromResult(InMemoryData.Products.AsEnumerable());
        }

        public Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            var products = InMemoryData.Products
                .Where(p => p.Category.Equals(category, System.StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(products);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var product = InMemoryData.Products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }
    }
}