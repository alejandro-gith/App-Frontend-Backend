namespace MiApp.Application.DTOs
{
    // US07: datos que envía el frontend para editar un producto existente.
    // No incluye el ID: viaja en la URL (PUT api/products/{id}) y el cliente no debe poder cambiarlo.
    public class UpdateProductRequest
    {
        // Nuevo título del producto
        public string Title { get; set; } = string.Empty;

        // Nuevo precio (debe ser mayor a cero)
        public decimal Price { get; set; }

        // Nueva descripción
        public string Description { get; set; } = string.Empty;

        // Nueva URL de la imagen
        public string Image { get; set; } = string.Empty;

        // Nueva categoría
        public string Category { get; set; } = string.Empty;
    }
}