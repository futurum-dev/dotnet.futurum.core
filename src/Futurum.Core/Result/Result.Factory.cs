using System.Diagnostics;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    private static readonly Result ResultOk = new(true);
    private static readonly Task<Result> ResultOkAsync = Task.FromResult(ResultOk);

    /// <summary>
    /// Create a <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    /// </summary>
    [DebuggerStepThrough]
    public static Result Ok() =>
        ResultOk;

    /// <summary>
    /// Create a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true and value.
    /// </summary>
    [DebuggerStepThrough]
    public static Result<T> Ok<T>(T value) =>
        new(value);

    /// <summary>
    /// Create a <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    /// </summary>
    [DebuggerStepThrough]
    public static Task<Result> OkAsync() =>
        ResultOkAsync;

    /// <summary>
    /// Create a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true and value.
    /// </summary>
    [DebuggerStepThrough]
    public static Task<Result<T>> OkAsync<T>(T value) =>
        Task.FromResult(new Result<T>(value));

    /// <summary>
    /// Create a <see cref="Result"/> with <see cref="Result.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Result Fail(string error) =>
        new(error.ToResultError());

    /// <summary>
    /// Create a <see cref="Result"/> with <see cref="Result.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Result Fail(IResultError error) =>
        new(error);

    /// <summary>
    /// Create a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Result<T> Fail<T>(string error) =>
        new(error.ToResultError());

    /// <summary>
    /// Create a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Result<T> Fail<T>(IResultError error) =>
        new(error);

    /// <summary>
    /// Create a <see cref="Result"/> with <see cref="Result.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Task<Result> FailAsync(string error) =>
        Task.FromResult(new Result(error.ToResultError()));

    /// <summary>
    /// Create a <see cref="Result"/> with <see cref="Result.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Task<Result> FailAsync(IResultError error) =>
        Task.FromResult(new Result(error));

    /// <summary>
    /// Create a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Task<Result<T>> FailAsync<T>(string error) =>
        Task.FromResult(new Result<T>(error.ToResultError()));

    /// <summary>
    /// Create a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true and error. 
    /// </summary>
    [DebuggerStepThrough]
    public static Task<Result<T>> FailAsync<T>(IResultError error) =>
        Task.FromResult(new Result<T>(error));
}