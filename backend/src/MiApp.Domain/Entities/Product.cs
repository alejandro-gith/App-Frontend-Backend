namespace MiApp.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Category { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;
}