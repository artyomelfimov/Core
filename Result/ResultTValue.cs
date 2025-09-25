namespace Core.Result;

public class Result<TValue> : Result
{
    internal Result(TValue value, bool isSuccess, string errorMessage = null)
      : base(isSuccess, errorMessage)
    {
        Value = value;
    }

    public TValue Value { get; }
}
