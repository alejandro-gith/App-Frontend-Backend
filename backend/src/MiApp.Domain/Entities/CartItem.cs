namespace MiApp.Domain.Entities;

public class CartItem
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public string Image { get; set; } = string.Empty;
}