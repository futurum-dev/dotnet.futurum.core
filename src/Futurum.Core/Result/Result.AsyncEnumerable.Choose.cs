namespace Futurum.Core.Result;

public static partial class ResultAsyncEnumerableExtensions
{
    /// <summary>
    /// Return only those elements of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsFailure"/> true is returned.
    /// </summary>
    public static IAsyncEnumerable<TSource> Choose<TSource>(this IAsyncEnumerable<Result<TSource>> source) =>
        source.Where(Result.FilterSuccess)
              .Select(ResultExtensions.GetValue);

    /// <summary>
    /// Return only those elements of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsFailure"/> true is returned.
    /// </summary>
    public static IAsyncEnumerable<TR> Choose<TSource, TR>(this IAsyncEnumerable<TSource> source, Func<TSource, Result<TR>> selectorFunc) =>
        source.Select(selectorFunc)
              .Choose();
}