namespace Futurum.Core.Option;

public readonly partial struct Option<T>
{
    /// <summary>
    /// If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then return <see cref="Option{T}"/> of <paramref name="orElseOption"/>.
    /// </summary>
    public Option<T> OrElse(T orElseOption) =>
        HasValue ? Value : orElseOption;

    /// <summary>
    /// If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then return <see cref="Option{T}"/> of <paramref name="orElseOptionFunc"/> called.
    /// </summary>
    public Option<T> OrElse(Func<T> orElseOptionFunc) =>
        HasValue ? Value : orElseOptionFunc();

    /// <summary>
    /// If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then return <see cref="Option{T}"/> of <paramref name="orElseOption"/>.
    /// </summary>
    public Option<T> OrElse(Option<T> orElseOption) =>
        HasValue ? Value : orElseOption;

    /// <summary>
    /// If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then return <see cref="Option{T}"/> of <paramref name="orElseOptionFunc"/> called.
    /// </summary>
    public Option<T> OrElse(Func<Option<T>> orElseOptionFunc) =>
        HasValue ? Value : orElseOptionFunc();
}