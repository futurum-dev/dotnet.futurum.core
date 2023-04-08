namespace Futurum.Core.Result;

public static partial class ResultDictionaryExtensions
{
    /// <summary>
    /// Get the value associated with the specified key as an Result.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the key is found, return the value as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the key is not found, return <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, string errorMessage)
        where TKey : notnull =>
        source.TryGetValue(key, out var value) ? Result.Ok(value) : Result.Fail<TValue>(errorMessage);

    /// <summary>
    /// Get the value associated with the specified key as an Result.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the key is found, return the value as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the key is not found, return <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, Func<string> errorMessage)
        where TKey : notnull =>
        source.TryGetValue(key, out var value) ? Result.Ok(value) : Result.Fail<TValue>(errorMessage());

    /// <summary>
    /// Get the value associated with the specified key as an Result.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the key is found, return the value as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the key is not found, return <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key, string errorMessage)
        where TKey : notnull =>
        source.TryGetValue(key, out var value) ? Result.Ok(value) : Result.Fail<TValue>(errorMessage);

    /// <summary>
    /// Get the value associated with the specified key as an Result.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the key is found, return the value as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the key is not found, return <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key, Func<string> errorMessage)
        where TKey : notnull =>
        source.TryGetValue(key, out var value) ? Result.Ok(value) : Result.Fail<TValue>(errorMessage());
}