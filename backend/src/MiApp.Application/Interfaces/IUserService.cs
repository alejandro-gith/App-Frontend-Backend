using MiApp.Application.DTOs;

namespace MiApp.Application.Interfaces;

public interface IUserService
{
    IEnumerable<UserResponse> GetAll();
}