namespace DDD.Results.CommonKit;

public class Result
{
    protected static Error NullValueError => new(500, "null.value", "The specified result value is null.");

    protected Result() { }
    protected Result(Error error)
    {
        Error = error;
    }
    public bool IsSuccess => Error is null;

    public bool IsFailed => !IsSuccess;

    public Error? Error { get; }

    public static Result Success() => new();

    public static Result<TValue> Success<TValue>(TValue value) => Create(value);

    public static Result Fail(Error error) => new(error);

    public static Result<TValue> Fail<TValue>(Error error) => new(error);

    protected static Result<TValue> Create<TValue>(TValue? value) => value is not null ? new(value) : Fail<TValue>(NullValueError);
}