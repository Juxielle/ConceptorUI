namespace ConceptorUI.Domain.ValueObjects;

public class Result<TValue>
{
    public bool IsSuccess { get; set; }
    public TValue Value { get; set; }
    public Error Error { get; set; }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>
        {
            IsSuccess = true,
            Value = value,
            Error = Error.None
        };
    }

    public static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>
        {
            IsSuccess = false,
            Value = default!,
            Error = error
        };
    }
}