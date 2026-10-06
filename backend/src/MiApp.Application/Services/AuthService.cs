using MiApp.Application.Common;
using MiApp.Application.DTOs.Auth;
using MiApp.Application.Interfaces;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public Result<LoginResponse> Login(LoginRequest request)
    {
        // Validar usuario vacío
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            return Result<LoginResponse>.Failure(
                "El usuario es obligatorio."
            );
        }

        // Validar contraseña vacía
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<LoginResponse>.Failure(
                "La contraseña es obligatoria."
            );
        }

        // Buscar usuario
        var user = _userRepository.GetByUsername(
            request.Username.Trim()
        );

        // Validar existencia del usuario
        if (user is null)
        {
            return Result<LoginResponse>.Failure(
                "Usuario o contraseña incorrectos."
            );
        }

        // Validar contraseña
        if (user.Password != request.Password)
        {
            return Result<LoginResponse>.Failure(
                "Usuario o contraseña incorrectos."
            );
        }

        // Generar JWT
        var token = _tokenService.GenerateToken(user);

        var response = new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role.ToString(),
            Token = token
        };

        return Result<LoginResponse>.Success(response);
    }
}