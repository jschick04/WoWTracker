namespace Tracker.Library.Helpers;

public class Result : IResult
{
    public string Message { get; set; } = null!;

    public bool Succeeded { get; set; }

    public static IResult Fail() => new Result { Succeeded = false };

    public static IResult Fail(string? message) => new Result { Succeeded = false, Message = message ?? string.Empty };

    public static Task<IResult> FailAsync() => Task.FromResult(Fail());

    public static Task<IResult> FailAsync(string? message) => Task.FromResult(Fail(message));

    public static IResult Success() => new Result { Succeeded = true };

    public static IResult Success(string? message) =>
        new Result { Succeeded = true, Message = message ?? string.Empty };

    public static Task<IResult> SuccessAsync() => Task.FromResult(Success());

    public static Task<IResult> SuccessAsync(string? message) => Task.FromResult(Success(message));
}

public class Result<T> : Result, IResult<T>
{
    public T? Data { get; set; }

    public new static Result<T> Fail() => new() { Succeeded = false };

    public new static Result<T> Fail(string? message) => new() { Succeeded = false, Message = message ?? string.Empty };

    public new static Task<Result<T>> FailAsync() => Task.FromResult(Fail());

    public new static Task<Result<T>> FailAsync(string? message) => Task.FromResult(Fail(message));

    public new static Result<T> Success() => new() { Succeeded = true };

    public new static Result<T> Success(string? message) =>
        new() { Succeeded = true, Message = message ?? string.Empty };

    public static Result<T> Success(T? data) => new() { Succeeded = true, Data = data };

    public static Result<T> Success(T? data, string? message) =>
        new() { Succeeded = true, Data = data, Message = message ?? string.Empty };

    public new static Task<Result<T>> SuccessAsync() => Task.FromResult(Success());

    public new static Task<Result<T>> SuccessAsync(string? message) => Task.FromResult(Success(message));

    public static Task<Result<T>> SuccessAsync(T? data) => Task.FromResult(Success(data));

    public static Task<Result<T>> SuccessAsync(T? data, string? message) => Task.FromResult(Success(data, message));
}
