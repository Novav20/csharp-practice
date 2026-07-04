using System.Diagnostics.CodeAnalysis;
public record Result<TValue, TError>
{
    public TValue? Value { get; }
    public TError? Error { get; }

    // Tell the compiler: If IsSuccess is true, Value is NOT null
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess { get; }

    // Tell the compiler: If IsFailure is true, Error is NOT null
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;

    private Result(TValue value)
    {
        Value = value;
        Error = default;
        IsSuccess = true;
    }

    private Result(TError error)
    {
        Value = default;
        Error = error;
        IsSuccess = false;
    }

    // Factory Methods
    public static Result<TValue, TError> Success(TValue value) => new(value);
    public static Result<TValue, TError> Failure(TError error) => new(error);
}