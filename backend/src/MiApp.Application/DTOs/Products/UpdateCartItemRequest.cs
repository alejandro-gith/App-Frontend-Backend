namespace MiApp.Application.DTOs;

public class UpdateCartItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}