namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Return the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsFailure"/> true is returned.
    /// </summary>
    public static Result<TSource> Pick<TSource>(this IEnumerable<Result<TSource>> source, string errorMessage) =>
        source.Pick(errorMessage.ToResultError());

    /// <summary>
    /// Return the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsFailure"/> true is returned.
    /// </summary>
    public static Result<TSource> Pick<TSource>(this IEnumerable<Result<TSource>> source, IResultError error) =>
        source.Where(Result.FilterSuccess)
              .FirstOrDefault(Result.Fail<TSource>(error));

    /// <summary>
    /// Return the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsFailure"/> true is returned.
    /// </summary>
    public static Result<TR> Pick<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Result<TR>> selectorFunc, string errorMessage) =>
        source.Pick(selectorFunc, errorMessage.ToResultError());

    /// <summary>
    /// Return the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsFailure"/> true is returned.
    /// </summary>
    public static Result<TR> Pick<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Result<TR>> selectorFunc, IResultError error) =>
        source.Select(selectorFunc)
              .Pick(error);
}