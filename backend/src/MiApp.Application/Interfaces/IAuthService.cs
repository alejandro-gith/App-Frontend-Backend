using MiApp.Application.Common;
using MiApp.Application.DTOs.Auth;

namespace MiApp.Application.Interfaces;

public interface IAuthService
{
    Result<LoginResponse> Login(LoginRequest request);
}