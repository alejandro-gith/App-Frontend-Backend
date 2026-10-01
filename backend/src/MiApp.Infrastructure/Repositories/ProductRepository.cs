using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Data;

namespace MiApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAll()
    {
        return InMemoryData.Products;
    }

    public Product? GetById(int id)
    {
        return InMemoryData.Products
            .FirstOrDefault(product => product.Id == id);
    }

    public Product Add(Product product)
    {
        product.Id = InMemoryData.Products.Count == 0
            ? 1
            : InMemoryData.Products.Max(p => p.Id) + 1;

        InMemoryData.Products.Add(product);

        return product;
    }

    public bool Update(Product product)
    {
        var existingProduct = GetById(product.Id);

        if (existingProduct is null)
            return false;

        existingProduct.Title = product.Title;
        existingProduct.Price = product.Price;
        existingProduct.Category = product.Category;
        existingProduct.Description = product.Description;
        existingProduct.Image = product.Image;

        return true;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);

        if (product is null)
            return false;

        return InMemoryData.Products.Remove(product);
    }
}