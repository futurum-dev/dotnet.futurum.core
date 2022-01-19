namespace Futurum.Core.Linq;

public static partial class StringEnumerableExtensions
{
    public static string StringJoin<T>(this IEnumerable<T> values, string seperator) =>
        string.Join(seperator, values);
}