﻿namespace Tracker.Library.Helpers;

public class Result : IResult
{
    public List<string>? Messages { get; set; }

    public bool Succeeded { get; set; }

    public static IResult Fail() => new Result { Succeeded = false };

    public static IResult Fail(string? message) =>
        new Result { Succeeded = false, Messages = new List<string> { message ?? string.Empty } };

    public static IResult Fail(List<string>? messages) => new Result { Succeeded = false, Messages = messages };

    public static Task<IResult> FailAsync() => Task.FromResult(Fail());

    public static Task<IResult> FailAsync(string? message) => Task.FromResult(Fail(message));

    public static Task<IResult> FailAsync(List<string>? messages) => Task.FromResult(Fail(messages));

    public static IResult Success() => new Result { Succeeded = true };

    public static IResult Success(string? message) =>
        new Result { Succeeded = true, Messages = new List<string> { message ?? string.Empty } };

    public static Task<IResult> SuccessAsync() => Task.FromResult(Success());

    public static Task<IResult> SuccessAsync(string? message) => Task.FromResult(Success(message));
}

public class Result<T> : Result, IResult<T>
{
    public T? Data { get; set; }

    public new static Result<T> Fail() => new() { Succeeded = false };

    public new static Result<T> Fail(string? message) =>
        new() { Succeeded = false, Messages = new List<string> { message ?? string.Empty } };

    public new static Result<T> Fail(List<string>? messages) =>
        new() { Succeeded = false, Messages = messages ?? new List<string>() };

    public new static Task<Result<T>> FailAsync() => Task.FromResult(Fail());

    public new static Task<Result<T>> FailAsync(string? message) => Task.FromResult(Fail(message));

    public new static Task<Result<T>> FailAsync(List<string>? messages) => Task.FromResult(Fail(messages));

    public new static Result<T> Success() => new() { Succeeded = true };

    public new static Result<T> Success(string? message) =>
        new() { Succeeded = true, Messages = new List<string> { message ?? string.Empty } };

    public static Result<T> Success(T? data) => new() { Succeeded = true, Data = data };

    public static Result<T> Success(T? data, string? message) =>
        new() { Succeeded = true, Data = data, Messages = new List<string> { message ?? string.Empty } };

    public static Result<T> Success(T? data, List<string>? messages) =>
        new() { Succeeded = true, Data = data, Messages = messages ?? new List<string>() };

    public new static Task<Result<T>> SuccessAsync() => Task.FromResult(Success());

    public new static Task<Result<T>> SuccessAsync(string? message) => Task.FromResult(Success(message));

    public static Task<Result<T>> SuccessAsync(T? data) => Task.FromResult(Success(data));

    public static Task<Result<T>> SuccessAsync(T? data, string? message) => Task.FromResult(Success(data, message));
}
