using MiApp.Application.DTOs;

namespace MiApp.Application.Interfaces;

public interface ICartQueryService
{
    IEnumerable<CartResponse> GetAll();
}