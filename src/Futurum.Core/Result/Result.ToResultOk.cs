namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms a <see cref="T" /> to a <see cref="Result{T}" /> with <see cref="Result{T}.IsSuccess"/> true.
    /// </summary>
    public static Result<T> ToResultOk<T>(this T value) =>
        Result.Ok(value);

    /// <summary>
    /// Transforms a <see cref="T" /> to an async <see cref="Result{T}" /> with <see cref="Result{T}.IsSuccess"/> true.
    /// </summary>
    public static Task<Result<T>> ToResultOkAsync<T>(this T value) =>
        Result.OkAsync(value);
}