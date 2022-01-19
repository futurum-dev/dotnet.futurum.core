namespace Futurum.Core.Linq;

public static partial class EnumerableExtensions
{
    public static List<TSource> AsList<TSource>(this IEnumerable<TSource> source) =>
        source switch
        {
            List<TSource> list => list,
            _                  => source.ToList()
        };
}