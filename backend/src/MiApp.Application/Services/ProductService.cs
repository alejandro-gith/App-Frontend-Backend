using MiApp.Application.Common;
using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using System; // US06: necesario para validar la URL de la imagen (Uri)
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

        // US06: crea un producto nuevo. Aquí viven las reglas de negocio y validaciones.
        // El backend valida SIEMPRE, aunque Angular ya haya validado el formulario.
        // Recibe: CreateProductRequest. Devuelve: Result con el ProductDto creado, o un mensaje de error.
        public async Task<Result<ProductDto>> CreateProductAsync(CreateProductRequest request)
        {
            // Título obligatorio
            if (string.IsNullOrWhiteSpace(request.Title))
                return Result<ProductDto>.Failure("El título del producto es obligatorio.");

            // Título con longitud máxima razonable
            if (request.Title.Trim().Length > 100)
                return Result<ProductDto>.Failure("El título no puede superar los 100 caracteres.");

            // Precio mayor a cero
            if (request.Price <= 0)
                return Result<ProductDto>.Failure("El precio del producto debe ser mayor a cero.");

            // Descripción obligatoria
            if (string.IsNullOrWhiteSpace(request.Description))
                return Result<ProductDto>.Failure("La descripción del producto es obligatoria.");

            // Categoría obligatoria
            if (string.IsNullOrWhiteSpace(request.Category))
                return Result<ProductDto>.Failure("La categoría del producto es obligatoria.");

            // La imagen debe ser una URL absoluta con http o https
            if (!Uri.TryCreate(request.Image, UriKind.Absolute, out var imageUri)
                || (imageUri.Scheme != Uri.UriSchemeHttp && imageUri.Scheme != Uri.UriSchemeHttps))
                return Result<ProductDto>.Failure("La imagen debe ser una URL válida (http o https).");

            // Convierte el DTO (lo que manda el cliente) en la entidad del dominio.
            // Nota: el DTO usa Title/Image y la entidad usa Name/ImageUrl.
            var product = new Product
            {
                Name = request.Title.Trim(),
                Price = request.Price,
                Description = request.Description.Trim(),
                ImageUrl = request.Image.Trim(),
                Category = request.Category.Trim()
                // Stock queda en 0: la historia no lo pide
            };

            // El repositorio guarda el producto y le asigna el ID
            var createdProduct = await _productRepository.AddAsync(product);

            // Devuelve solo los datos necesarios al cliente (DTO), no la entidad completa
            var dto = new ProductDto
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                Category = createdProduct.Category,
                ImageUrl = createdProduct.ImageUrl
            };

            return Result<ProductDto>.Success(dto);
        }
    }
}