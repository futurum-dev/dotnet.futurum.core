namespace Futurum.Core.Option;

public readonly partial struct Option<T>
{
    /// <summary>
    /// Perform side-effect on a <see cref="Option{T}"/>. Always returning the original <see cref="Option{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}"/> has no value, then call <paramref name="hasNoValueFunc"/>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}"/> has a value, then call <paramref name="hasValueFunc"/>
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Option<T> DoSwitch(Action<T> hasValueFunc, Action hasNoValueFunc)
    {
        if (HasNoValue)
            hasNoValueFunc();
        else
            hasValueFunc(Value);

        return this;
    }
}