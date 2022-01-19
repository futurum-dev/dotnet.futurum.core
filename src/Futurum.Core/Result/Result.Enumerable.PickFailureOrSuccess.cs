namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Return the first element of a sequence based on <see cref="Result.FilterFailure"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Result"/> with <see cref="Result.IsSuccess"/> true is returned.
    /// </summary>
    public static Result PickFailureOrSuccess(this IEnumerable<Result> source) =>
        source.Where(Result.FilterFailure)
              .FirstOrDefault(Result.Ok());
}