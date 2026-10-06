using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiApp.Application.Interfaces;
using MiApp.Application.Services;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Repositories;
using MiApp.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Registro de Servicios (ANTES de builder.Build()) ---
builder.Services.AddControllers();

// Inyección de Dependencias
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

// Consulta global de carritos — US12
builder.Services.AddScoped<ICartQueryService, CartQueryService>();
builder.Services.AddScoped<ICartQueryRepository, CartQueryRepository>();

// Configuración de JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("La clave JWT no está configurada.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            )
        };
    });

builder.Services.AddAuthorization();

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

// Activar CORS
app.UseCors("Frontend");

// Middlewares de Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear los Controladores de la API
app.MapControllers();

app.Run();