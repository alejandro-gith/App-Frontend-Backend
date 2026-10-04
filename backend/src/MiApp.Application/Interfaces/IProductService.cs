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

        // US06: crea un producto nuevo a partir de los datos del formulario
        Task<Result<ProductDto>> CreateProductAsync(CreateProductRequest request);

        // US07: edita el producto con el id indicado. Devuelve el detalle actualizado,
        // o un fallo "no encontrado" / "datos inválidos".
        Task<Result<ProductDetailDto>> UpdateProductAsync(int id, UpdateProductRequest request);
    }
}