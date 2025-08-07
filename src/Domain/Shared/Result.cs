using System.Diagnostics.CodeAnalysis;

namespace KgvSystem.Domain.Shared;

/// <summary>
/// Represents the result of an operation that can fail.
/// Used for railway-oriented programming pattern throughout the domain.
/// </summary>
public sealed class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    private Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
}

/// <summary>
/// Represents the result of an operation that can fail and returns a value on success.
/// </summary>
/// <typeparam name="T">The type of the value returned on success</typeparam>
public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public string Error { get; }

    private Result(bool isSuccess, T value, string error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, string.Empty);
    public static Result<T> Failure(string error) => new(false, default!, error);

    /// <summary>
    /// Executes the provided action if the result is successful
    /// </summary>
    public Result<T> OnSuccess(Action<T> action)
    {
        if (IsSuccess)
            action(Value);
        return this;
    }

    /// <summary>
    /// Executes the provided action if the result is a failure
    /// </summary>
    public Result<T> OnFailure(Action<string> action)
    {
        if (IsFailure)
            action(Error);
        return this;
    }

    /// <summary>
    /// Maps the result to a different type if successful
    /// </summary>
    public Result<TResult> Map<TResult>(Func<T, TResult> mapper)
    {
        return IsSuccess 
            ? Result<TResult>.Success(mapper(Value))
            : Result<TResult>.Failure(Error);
    }

    /// <summary>
    /// Binds the result to another result-returning operation
    /// </summary>
    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> binder)
    {
        return IsSuccess ? binder(Value) : Result<TResult>.Failure(Error);
    }

    /// <summary>
    /// Implicitly converts to Result for method chaining
    /// </summary>
    public static implicit operator Result(Result<T> result)
    {
        return result.IsSuccess ? Result.Success() : Result.Failure(result.Error);
    }
}