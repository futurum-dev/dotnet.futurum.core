namespace Futurum.Core.Option;

public static partial class Option
{
    /// <summary>
    /// Predicate for <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> true. 
    /// </summary>
    public static bool FilterHasValue<T>(this Option<T> option) =>
        option.HasValue;

    /// <summary>
    /// Predicate for <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> false. 
    /// </summary>
    public static bool FilterHasNoValue<T>(this Option<T> option) =>
        !option.HasValue;
}