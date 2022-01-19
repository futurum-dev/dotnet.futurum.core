namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Transforms each element of a sequence based  on whether it <see cref="Option{T}.HasValue"/> or not.
    /// </summary>
    public static IEnumerable<TR> MapSwitch<TSource, TR>(this IEnumerable<Option<TSource>> source, Func<TSource, TR> hasValueFunc, Func<TR> hasNoValueFunc)
    {
        TR Switch(Option<TSource> option) => option.Switch(hasValueFunc, hasNoValueFunc);

        return source.Select(Switch);
    }
}