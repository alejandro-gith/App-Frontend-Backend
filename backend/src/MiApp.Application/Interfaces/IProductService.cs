using MiApp.Application.Common;
using MiApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetCatalogAsync();
        Task<Result<IEnumerable<ProductDto>>> GetCatalogByCategoryAsync(string category);
        Task<Result<ProductDetailDto>> GetProductDetailAsync(int id);
    }
}