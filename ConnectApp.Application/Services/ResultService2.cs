using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Services
{
    public abstract class ResultService2
    {
        protected static Result<T> Success<T>(T data, string message = "Sucesso") => Result<T>.Ok(data, message);
        protected static Result<T> Failure<T>(string message, params string[] errors) => Result<T>.Failure(message, errors);
    }
}
