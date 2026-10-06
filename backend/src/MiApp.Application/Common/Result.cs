namespace MiApp.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        // US07: es true cuando el fallo se debe a que el recurso no existe.
        // El controller lo traduce a HTTP 404 (en vez del 400 de datos inválidos).
        public bool IsNotFound { get; }

        private Result(bool isSuccess, T? value, string? error, bool isNotFound = false)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            IsNotFound = isNotFound;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);

        // US07: crea un resultado fallido de tipo "no encontrado"
        public static Result<T> NotFound(string error) => new Result<T>(false, default, error, true);
    }
}