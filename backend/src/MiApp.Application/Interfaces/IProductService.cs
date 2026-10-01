using MiApp.Application.Common;
using MiApp.Application.DTOs.Products;
using MiApp.Domain.Entities;

namespace MiApp.Application.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetAll();

    Product? GetById(int id);

    Result<Product> Create(CreateProductRequest request);
}