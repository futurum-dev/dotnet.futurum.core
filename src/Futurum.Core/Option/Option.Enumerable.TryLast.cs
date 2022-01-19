namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Returns the last element of a sequence.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the sequence is empty, then an <see cref="Option{TSource}"/>.None is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the sequence is not empty, the last element is returned as a <see cref="Option{T}"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<TSource> TryLast<TSource>(this IEnumerable<TSource> source) =>
        source.Select(Option<TSource>.From)
              .LastOrDefault();

    /// <summary>
    /// Returns the last element of a sequence.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the sequence is empty, then an <see cref="Option{TSource}"/>.None is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the sequence is not empty, the last element is returned as a <see cref="Option{T}"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<TSource> TryLast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) =>
        source.Where(predicate)
              .TryLast();
}