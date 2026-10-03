using MiApp.Application.Common;
using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetCatalogAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                ImageUrl = p.ImageUrl
            });
            return Result<IEnumerable<ProductDto>>.Success(dtos);
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetCatalogByCategoryAsync(string category)
        {
            var products = await _productRepository.GetByCategoryAsync(category);
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                ImageUrl = p.ImageUrl
            });
            return Result<IEnumerable<ProductDto>>.Success(dtos);
        }

        public async Task<Result<ProductDetailDto>> GetProductDetailAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return Result<ProductDetailDto>.Failure("Producto no encontrado.");

            var dto = new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock
            };
            return Result<ProductDetailDto>.Success(dto);
        }
    }
}