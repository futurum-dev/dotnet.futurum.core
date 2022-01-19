namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms a <paramref name="error"/> to a <see cref="Result" /> with <see cref="Result"/> <see cref="Result.IsFailure"/> true.
    /// </summary>
    public static Result ToFailResult(this string error) =>
        Result.Fail(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to a <see cref="Result{T}" /> with <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> true.
    /// </summary>
    public static Result<T> ToFailResult<T>(this string error) =>
        Result.Fail<T>(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to a <see cref="Result" /> with <see cref="Result"/> <see cref="Result.IsFailure"/> true.
    /// </summary>
    public static Result ToFailResult(this IResultError error) =>
        Result.Fail(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to a <see cref="Result{T}" /> with <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> true.
    /// </summary>
    public static Result<T> ToFailResult<T>(this IResultError error) =>
        Result.Fail<T>(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to an <see cref="Result" /> with <see cref="Result"/> <see cref="Result.IsFailure"/> true.
    /// </summary>
    public static Task<Result> ToFailResultAsync(this string error) =>
        Result.FailAsync(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to an <see cref="Result{T}" /> with <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> true.
    /// </summary>
    public static Task<Result<T>> ToFailResultAsync<T>(this string error) =>
        Result.FailAsync<T>(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to an <see cref="Result" /> with <see cref="Result"/> <see cref="Result.IsFailure"/> true.
    /// </summary>
    public static Task<Result> ToFailResultAsync(this IResultError error) =>
        Result.FailAsync(error);

    /// <summary>
    /// Transforms a <paramref name="error"/> to an <see cref="Result{T}" /> with <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> true.
    /// </summary>
    public static Task<Result<T>> ToFailResultAsync<T>(this IResultError error) =>
        Result.FailAsync<T>(error);
}