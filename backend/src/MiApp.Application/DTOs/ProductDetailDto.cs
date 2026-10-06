namespace MiApp.Application.DTOs
{
    public class ProductDetailDto : ProductDto
    {
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}