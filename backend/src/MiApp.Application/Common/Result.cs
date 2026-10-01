namespace MiApp.Application.Common;

public class Result<T>
{
    public bool Success { get; init; }

    public string? Message { get; init; }

    public T? Data { get; init; }

    public static Result<T> Ok(T data, string? message = null)
    {
        return new Result<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static Result<T> Fail(string message)
    {
        return new Result<T>
        {
            Success = false,
            Message = message
        };
    }
}