namespace Futurum.Core.Option;

public static partial class OptionDictionaryExtensions
{
    /// <summary>
    /// Get the value associated with the specified key as an Option.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the key is found, return the value as <see cref="Option{T}"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the key is not found, return the value as <see cref="Option{T}"/> <see cref="Option{T}.None"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        where TKey : notnull =>
        source.TryGetValue(key, out var value) ? Option<TValue>.From(value) : Option<TValue>.None;

    /// <summary>
    /// Get the value associated with the specified key as an Option.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the key is found, return the value as <see cref="Option{T}"/>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the key is not found, return the value as <see cref="Option{T}"/> <see cref="Option{T}.None"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Option<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
        where TKey : notnull =>
        source.TryGetValue(key, out var value) ? Option<TValue>.From(value) : Option<TValue>.None;
}