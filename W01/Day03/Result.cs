using System.Diagnostics.CodeAnalysis;
public class Result<TValue, TError>
{
    public TValue? Value { get; }
    public TError? Error { get; }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess { get; }

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

    public static Result<TValue, TError> Success(TValue value) => new(value);
    public static Result<TValue, TError> Failure(TError error) => new(error);
}