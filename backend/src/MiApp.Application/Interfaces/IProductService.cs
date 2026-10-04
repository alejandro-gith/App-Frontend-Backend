using MiApp.Application.Common;
using MiApp.Application.DTOs; // <--- Asegúrate de que esta línea esté presente
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetCatalogAsync();
        Task<Result<IEnumerable<ProductDto>>> GetCatalogByCategoryAsync(string category);
        Task<Result<ProductDetailDto>> GetProductDetailAsync(int id);
        Task<Result<ProductDto>> CreateProductAsync(CreateProductRequest request);
    }
}