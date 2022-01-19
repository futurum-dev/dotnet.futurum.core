namespace Futurum.Core.Option;

public readonly partial struct Option<T>
{
    /// <summary>
    /// Transforms <see cref="Option{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true, then call and return <paramref name="hasValueFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then call and return <paramref name="hasNoValueFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public TR Switch<TR>(Func<T, TR> hasValueFunc, Func<TR> hasNoValueFunc) =>
        HasValue ? hasValueFunc(Value) : hasNoValueFunc();

    /// <summary>
    /// Transforms <see cref="Option{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true, then call and return <paramref name="hasValueFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false, then call and return <paramref name="hasNoValueFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public Option<TR> Switch<TR>(Func<T, bool> predicate, Func<T, TR> hasValueFunc, Func<T, TR> hasNoValueFunc) =>
        HasValue ? predicate(Value) ? hasValueFunc(Value) : hasNoValueFunc(Value) : Option<TR>.None;
}