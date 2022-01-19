namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Return only those elements of a sequence based on <see cref="Option.FilterHasValue"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Option{TSource}"/>.None is returned.
    /// </summary>
    public static IEnumerable<TSource> Choose<TSource>(this IEnumerable<Option<TSource>> source) =>
        source.Where(Option.FilterHasValue)
              .Select(OptionExtensions.GetValue);

    /// <summary>
    /// Return only those elements of a sequence based on <see cref="Option.FilterHasValue"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then else <see cref="Option{TSource}"/>.None is returned.
    /// </summary>
    public static IEnumerable<TR> Choose<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Option<TR>> func) =>
        source.Select(func)
              .Choose();
}