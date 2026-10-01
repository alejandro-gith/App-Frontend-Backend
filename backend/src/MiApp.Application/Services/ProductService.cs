using MiApp.Application.Common;
using MiApp.Application.DTOs.Products;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetAll()
    {
        return _productRepository.GetAll();
    }

    public Product? GetById(int id)
    {
        return _productRepository.GetById(id);
    }

    public Result<Product> Create(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return Result<Product>.Fail("El título es obligatorio.");

        if (request.Price <= 0)
            return Result<Product>.Fail(
                "El precio debe ser mayor a cero."
            );

        if (string.IsNullOrWhiteSpace(request.Category))
            return Result<Product>.Fail(
                "La categoría es obligatoria."
            );

        if (string.IsNullOrWhiteSpace(request.Description))
            return Result<Product>.Fail(
                "La descripción es obligatoria."
            );

        if (!Uri.TryCreate(
                request.Image,
                UriKind.Absolute,
                out var imageUri) ||
            (imageUri.Scheme != Uri.UriSchemeHttp &&
             imageUri.Scheme != Uri.UriSchemeHttps))
        {
            return Result<Product>.Fail(
                "La URL de la imagen no es válida."
            );
        }

        var product = new Product
        {
            Title = request.Title.Trim(),
            Price = request.Price,
            Category = request.Category.Trim(),
            Description = request.Description.Trim(),
            Image = request.Image.Trim()
        };

        var createdProduct = _productRepository.Add(product);

        return Result<Product>.Ok(
            createdProduct,
            "Producto creado correctamente."
        );
    }
}