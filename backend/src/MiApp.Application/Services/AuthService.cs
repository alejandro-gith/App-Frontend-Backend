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
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            return Result<LoginResponse>.Fail(
                "El usuario es obligatorio."
            );
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<LoginResponse>.Fail(
                "La contraseña es obligatoria."
            );
        }

        var user = _userRepository.GetByUsername(
            request.Username.Trim()
        );

        if (user is null)
        {
            return Result<LoginResponse>.Fail(
                "Usuario o contraseña incorrectos."
            );
        }

        if (user.Password != request.Password)
        {
            return Result<LoginResponse>.Fail(
                "Usuario o contraseña incorrectos."
            );
        }

        var response = new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role.ToString(),
            Token = _tokenService.GenerateToken(user)
        };

        return Result<LoginResponse>.Ok(
            response,
            "Inicio de sesión correcto."
        );
    }
}