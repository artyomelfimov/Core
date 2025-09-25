namespace Core.Result;

public class Result
{
    protected Result(bool isSuccess, string errorMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }

    public bool IsSuccess { get; private set; }

    public bool IsFailure => !IsSuccess;

    public static Result Fail(string message) => new(false, message);

    public static Result<T> Fail<T>(string message) => new(default, false, message);

    public static Result Success() => new(true);

    public static Result<T> Success<T>(T value) => new(value, true);
}
