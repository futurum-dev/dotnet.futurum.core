namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms a <see cref="Result" /> to an async <see cref="Result" />.
    /// </summary>
    public static Task<Result> ToResultAsync(this Result result) =>
        Task.FromResult(result);
    
    /// <summary>
    /// Transforms a <see cref="Result{T}" /> to an async <see cref="Result{T}" />.
    /// </summary>
    public static Task<Result<T>> ToResultAsync<T>(this Result<T> result) =>
        Task.FromResult(result);
}