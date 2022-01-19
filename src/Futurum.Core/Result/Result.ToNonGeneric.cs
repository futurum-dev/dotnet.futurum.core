namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms a <see cref="Result{T}" /> to a <see cref="Result" />
    /// </summary>
    public static Result ToNonGeneric<T>(this Result<T> result) =>
        result.IsSuccess ? Result.Ok() : Result.Fail(result.Error.Value);

    /// <summary>
    /// Transforms an <see cref="Result{T}" /> to an <see cref="Result" />
    /// </summary>
    public static async Task<Result> ToNonGenericAsync<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;

        return result.IsSuccess ? Result.Ok() : Result.Fail(result.Error.Value);
    }
}