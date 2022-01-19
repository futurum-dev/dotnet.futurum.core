namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Returns the element at a specified index in a sequence.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the index is out of range, then an <see cref="Option{TSource}"/>.None is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the index is in range, the element at the index is returned as a <see cref="Option{T}"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<TSource> TryElementAt<TSource>(this IEnumerable<TSource> source, int index) =>
        source.Select(Option<TSource>.From)
              .ElementAtOrDefault(index);
}