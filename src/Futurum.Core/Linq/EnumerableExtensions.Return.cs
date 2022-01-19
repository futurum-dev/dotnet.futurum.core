namespace Futurum.Core.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<T> Return<T>(T value)
    {
        yield return value;
    }
}