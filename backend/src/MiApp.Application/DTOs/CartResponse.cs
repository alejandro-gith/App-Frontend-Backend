namespace MiApp.Application.DTOs;

public class CartResponse
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public List<CartItemResponse> Products { get; set; } = new();
}

public class CartItemResponse
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
