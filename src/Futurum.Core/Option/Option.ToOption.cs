namespace Futurum.Core.Option;

public static partial class OptionExtensions
{
    /// <summary>
    /// Transform an <see cref="T"/> to <see cref="Option{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <paramref name="value"/> is not null, then return <paramref name="value"/> as <see cref="Option{T}"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <paramref name="value"/> is null, then return <see cref="Option{T}"/> <see cref="Option{T}.None"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<T> ToOption<T>(this T? value) =>
        Option<T>.From(value);

    /// <summary>
    /// Transform an <see cref="Nullable{T}"/> to <see cref="Option{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <paramref name="nullableValue"/> has a value, then return <paramref name="nullableValue"/> as <see cref="Option{T}"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <paramref name="nullableValue"/> has no value, then return <see cref="Option{T}"/> <see cref="Option{T}.None"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<T> ToOption<T>(this T? nullableValue)
        where T : struct =>
        nullableValue.HasValue ? Option<T>.From(nullableValue.Value) : Option<T>.None;
}