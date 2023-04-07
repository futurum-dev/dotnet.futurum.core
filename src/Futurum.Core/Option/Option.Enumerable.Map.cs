namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Transforms each element of a sequence that matches <see cref="Option.FilterHasValue"/> predicate into a new form.
    /// </summary>
    public static IEnumerable<TR> Map<TSource, TR>(this IEnumerable<Option<TSource>> source, Func<TSource, TR> func) =>
        source.Where(Option.FilterHasValue)
              .Select(option => option.Value)
              .Select(func);
}