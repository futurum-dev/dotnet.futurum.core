namespace Futurum.Core.Option;

public static partial class Option
{
    /// <summary>
    /// Transform an <see cref="T"/> to <see cref="Option{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <paramref name="value"/> is not null, then return <see cref="Option{T}"/> with <see cref="Option{T}.HasValue"/> true.</description>
    ///     </item>
    ///     <item>
    ///         <description>If <paramref name="value"/> is null, then return <see cref="Option{T}"/> with <see cref="Option{T}.HasValue"/> false.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<T> From<T>(T? value) =>
        Option<T>.From(value);

    /// <summary>
    /// Returns an <see cref="Option{T}"/> with <see cref="Option{T}.HasValue"/> false.
    /// </summary>
    public static Option<T> None<T>() =>
        Option<T>.None;
}

public readonly partial struct Option<T>
{
    /// <summary>
    /// Returns an <see cref="Option{T}"/> with <see cref="Option{T}.HasValue"/> false.
    /// </summary>
    public static readonly Option<T> None = new();

    /// <summary>
    /// Transform an <see cref="T"/> to <see cref="Option{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <paramref name="value"/> is not null, then return <see cref="Option{T}"/> with <see cref="Option{T}.HasValue"/> true.</description>
    ///     </item>
    ///     <item>
    ///         <description>If <paramref name="value"/> is null, then return <see cref="Option{T}"/> with <see cref="Option{T}.HasValue"/> false.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<T> From(T? value) =>
        new(value);
}