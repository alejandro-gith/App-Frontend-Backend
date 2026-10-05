using MiApp.Application.Interfaces;
using MiApp.Application.Services;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Registro de Servicios (ANTES de builder.Build()) ---
builder.Services.AddControllers();

// Inyección de Dependencias - Productos
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Inyección de Dependencias - Carrito (NUEVAS LÍNEAS)
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

// Configuración de CORS para Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

// --- 2. Construcción de la App ---
var app = builder.Build();

// --- 3. Middlewares (DESPUÉS de builder.Build()) ---
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Activar CORS (debe ir antes de MapControllers)
app.UseCors("Frontend");

// Mapear los Controladores de la API
app.MapControllers();

app.Run();