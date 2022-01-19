namespace Futurum.Core.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<TSource> SelectMany<TSource>(this IEnumerable<IEnumerable<TSource>> source) =>
        source.SelectMany(x => x);
}