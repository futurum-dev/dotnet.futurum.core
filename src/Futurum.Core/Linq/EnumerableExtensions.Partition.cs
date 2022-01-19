namespace Futurum.Core.Linq;

public static partial class EnumerableExtensions
{
    public static (IEnumerable<T> matches, IEnumerable<T> nonMatches) Partition<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var groupedByMatching = source.ToLookup(predicate);

        return (groupedByMatching[true], groupedByMatching[false]);
    }
}