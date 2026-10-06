namespace MiApp.Application.DTOs
{
    // DTO (Data Transfer Object) que representa los datos que envía el frontend para crear un producto
    public class CreateProductRequest
    {
        // Título o nombre del producto proporcionado por el usuario
        public string Title { get; set; } = string.Empty;

        // Precio del producto (debe ser mayor a cero por reglas de negocio)
        public decimal Price { get; set; }

        // Descripción detallada del producto
        public string Description { get; set; } = string.Empty;

        // URL de la imagen del producto
        public string Image { get; set; } = string.Empty;

        // Categoría a la que pertenece el producto
        public string Category { get; set; } = string.Empty;
    }
}