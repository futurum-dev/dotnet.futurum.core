namespace Futurum.Core.Option;

public readonly partial struct Option<T>
{
    /// <summary>
    /// Transforms a <see cref="Option{T}"/> to <see cref="Option{TR}"/> using <paramref name="selector"/>
    /// </summary>
    public Option<TR> Map<TR>(Func<T, TR> selector) =>
        HasValue ? selector(Value) : Option.None<TR>();

    /// <summary>
    /// Transforms <see cref="Option{T}"/> to <see cref="Option{TR}"/> using <paramref name="selector"/>
    /// </summary>
    public Option<TR> Map<TR>(Func<T, Option<TR>> selector) =>
        HasValue ? selector(Value) : Option.None<TR>();
}