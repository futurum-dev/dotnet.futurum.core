namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Return the first element of a sequence based on <see cref="Option.FilterHasValue"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then Else <see cref="Option{TSource}"/>.None is returned.
    /// </summary>
    public static Option<TSource> Pick<TSource>(this IEnumerable<Option<TSource>> source) =>
        source.Where(Option.FilterHasValue)
              .FirstOrDefault(Option<TSource>.None);

    /// <summary>
    /// Return the first element of a sequence based on <see cref="Option.FilterHasValue"/> predicate.
    /// <para />
    /// If none of the elements in the sequence match the predicate, then Else <see cref="Option{TSource}"/>.None is returned.
    /// </summary>
    public static Option<TR> Pick<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Option<TR>> func) =>
        source.Select(func)
              .Pick();
}