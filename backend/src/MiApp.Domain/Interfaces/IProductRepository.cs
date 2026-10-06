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

        // US06: contrato para guardar un producto nuevo.
        // Recibe el producto sin ID y devuelve el mismo producto ya guardado, con su ID asignado.
        Task<Product> AddAsync(Product product);

        // US07: contrato para actualizar un producto existente (se identifica por su Id).
        // Devuelve true si lo encontró y lo actualizó, o false si no existe.
        Task<bool> UpdateAsync(Product product);

        // US08: contrato para eliminar el producto con el id indicado.
        // Devuelve true si lo eliminó, o false si no existía.
        Task<bool> DeleteAsync(int id);
    }
}