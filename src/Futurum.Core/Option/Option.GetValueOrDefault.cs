namespace Futurum.Core.Option;

public readonly partial struct Option<T>
{
    /// <summary>
    /// Get the value wrapped in an <see cref="Option{T}"/>, or uses the value from <paramref name="defaultValue"/>
    /// </summary>
    public T GetValueOrDefault(T defaultValue) =>
        HasValue ? Value : defaultValue;

    /// <summary>
    /// Get the value wrapped in an <see cref="Option{T}"/>, or uses the value from <paramref name="defaultValue"/>
    /// </summary>
    public TR GetValueOrDefault<TR>(Func<T, TR> selectorFunc, TR defaultValue) =>
        HasValue ? selectorFunc(Value) : defaultValue;

    /// <summary>
    /// Get the value wrapped in an <see cref="Option{T}"/>, or uses the value from <paramref name="defaultValue"/>
    /// </summary>
    public T GetValueOrDefault(Func<T> defaultValue) =>
        HasValue ? Value : defaultValue();

    /// <summary>
    /// Get the value wrapped in an <see cref="Option{T}"/>, or uses the value from <paramref name="defaultValue"/>
    /// </summary>
    public TR GetValueOrDefault<TR>(Func<T, TR> selectorFunc, Func<TR> defaultValue) =>
        HasValue ? selectorFunc(Value) : defaultValue();
}