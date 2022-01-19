namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Transforms each element of a sequence into a new form.
    /// </summary>
    public static IEnumerable<TR> Map<TSource, TR>(this IEnumerable<Option<TSource>> source, Func<Option<TSource>, TR> func) =>
        source.Select(func);
}