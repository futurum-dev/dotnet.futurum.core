namespace Futurum.Core.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T>? source) =>
        source ?? Enumerable.Empty<T>();
}