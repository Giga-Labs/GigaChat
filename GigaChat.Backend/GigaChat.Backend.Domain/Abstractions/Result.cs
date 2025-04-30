namespace GigaChat.Backend.Domain.Abstractions;

public class Result
{
    public bool Succeeded { get; }
    public bool Failed => !Succeeded;
    public Error Error = Error.None;

    public Result(bool succeeded, Error error)
    {
        if ((succeeded && error != Error.None) || (!succeeded && error == Error.None))
            throw new InvalidOperationException();

        Succeeded = succeeded;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new(true, Error.None, value);
    public static Result<TValue> Failure<TValue>(Error error) => new(false, error, default);
}

public class Result<TValue>(bool succeeded, Error error, TValue? value) : Result(succeeded, error)
{
    public TValue Value =>
        Succeeded ? value! : throw new InvalidOperationException("Failure results cannot contain a value");  // throw the exception since he is trying to access the Value while IsSuccess is false
}