using MiApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<Product?> GetByIdAsync(int id);
    }
}