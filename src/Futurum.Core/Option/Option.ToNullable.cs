namespace Futurum.Core.Option;

public static partial class OptionExtensions
{
    /// <summary>
    /// Transform an <see cref="Option{T}"/> to <see cref="Nullable{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true, then return <see cref="Option{T}"/>> <see cref="Option{T}.Value"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then return null</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static T? ToNullable<T>(this Option<T> option)
        where T : struct
    {
        static T? HasValue(T value) => value;

        static T? HasNoValue() => null;

        return option.Switch(HasValue, HasNoValue);
    }

    /// <summary>
    /// Transform an <see cref="Option{T}"/> to <see cref="Nullable{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true, then return <see cref="Option{T}"/>> <see cref="Option{T}.Value"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then return null</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static TR? ToNullable<T, TR>(this Option<T> option, Func<T, TR> selectorFunc)
        where TR : struct
    {
        TR? HasValue(T value) => selectorFunc(value);

        static TR? HasNoValue() => null;

        return option.Switch(HasValue, HasNoValue);
    }
}