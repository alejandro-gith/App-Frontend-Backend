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
            // Reglas compartidas con la edición (US07)
            var validationError = ValidateProductFields(
                request.Title, request.Price, request.Description, request.Category, request.Image);
            if (validationError != null)
                return Result<ProductDto>.Failure(validationError);

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

        // US07: edita un producto existente. Mantiene su ID y su stock; cambia solo los campos del formulario.
        // Recibe: el id (de la URL) y UpdateProductRequest (del cuerpo).
        // Devuelve: el detalle actualizado, "no encontrado" (404) o un mensaje de validación (400).
        public async Task<Result<ProductDetailDto>> UpdateProductAsync(int id, UpdateProductRequest request)
        {
            // Primero comprueba que el producto exista
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return Result<ProductDetailDto>.NotFound("Producto no encontrado.");

            // Mismas reglas de validación que al crear
            var validationError = ValidateProductFields(
                request.Title, request.Price, request.Description, request.Category, request.Image);
            if (validationError != null)
                return Result<ProductDetailDto>.Failure(validationError);

            // Aplica los cambios sobre el producto existente (conserva Id y Stock)
            existing.Name = request.Title.Trim();
            existing.Price = request.Price;
            existing.Description = request.Description.Trim();
            existing.ImageUrl = request.Image.Trim();
            existing.Category = request.Category.Trim();

            // El repositorio guarda el cambio; si ya no existía, lo tratamos como "no encontrado"
            var updated = await _productRepository.UpdateAsync(existing);
            if (!updated)
                return Result<ProductDetailDto>.NotFound("Producto no encontrado.");

            // Devuelve el detalle actualizado para que Angular refresque la pantalla
            var dto = new ProductDetailDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Description = existing.Description,
                Price = existing.Price,
                Category = existing.Category,
                ImageUrl = existing.ImageUrl,
                Stock = existing.Stock
            };

            return Result<ProductDetailDto>.Success(dto);
        }

        // US06/US07: validaciones comunes de los datos de un producto.
        // Devuelve el mensaje del primer error encontrado, o null si todo es válido.
        private static string? ValidateProductFields(
            string title, decimal price, string description, string category, string image)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "El título del producto es obligatorio.";

            if (title.Trim().Length > 100)
                return "El título no puede superar los 100 caracteres.";

            if (price <= 0)
                return "El precio del producto debe ser mayor a cero.";

            if (string.IsNullOrWhiteSpace(description))
                return "La descripción del producto es obligatoria.";

            if (string.IsNullOrWhiteSpace(category))
                return "La categoría del producto es obligatoria.";

            // La imagen debe ser una URL absoluta con http o https
            if (!Uri.TryCreate(image, UriKind.Absolute, out var imageUri)
                || (imageUri.Scheme != Uri.UriSchemeHttp && imageUri.Scheme != Uri.UriSchemeHttps))
                return "La imagen debe ser una URL válida (http o https).";

            return null;
        }
    }
}