namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    public static Result<IEnumerable<T>> EnsureThatAllSuccess<T>(this Result<IEnumerable<Result<T>>> results, Func<string> errorMessage) =>
        results.Then(Combine)
               .EnhanceWithError(errorMessage);

    public static Result<IEnumerable<T>> EnsureThatAllSuccess<T>(this Result<List<Result<T>>> results, Func<string> errorMessage) =>
        results.Then(Combine)
               .EnhanceWithError(errorMessage);
}