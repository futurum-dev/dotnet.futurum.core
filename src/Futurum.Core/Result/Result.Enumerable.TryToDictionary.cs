using Futurum.Core.Linq;

namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Try to create a <see cref="Dictionary{TKey, TElement}"/> from an <see cref="IEnumerable{T}"/> according to specified key selector and element selector functions.
    /// <para />
    /// Explicitly checks for duplicate keys.
    /// <para />
    /// If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If failure, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<Dictionary<TKey, TElement>> TryToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
                                                                                              Func<TSource, TKey> keySelector,
                                                                                              Func<TSource, TElement> elementSelector)
        where TKey : notnull
    {
        var duplicateKeys = source.Select(keySelector)
                                  .GroupBy(key => key)
                                  .Where(keyGrouping => keyGrouping.Count() > 1)
                                  .Select(keyGrouping => keyGrouping.Key)
                                  .Distinct()
                                  .ToList();

        if (duplicateKeys.Any())
        {
            return Result.Fail<Dictionary<TKey, TElement>>(
                $"{nameof(TryToDictionary)} failed as there are duplicate keys. Duplicate keys are : '{duplicateKeys.Select(x => $"'{x}'").StringJoin(",")}'");
        }

        return Result.Try(() => source.ToDictionary(keySelector, elementSelector), () => $"Failed to {nameof(Enumerable.ToDictionary)}");
    }

    /// <summary>
    /// Try to create a <see cref="Dictionary{TKey, TElement}"/> from an <see cref="IEnumerable{T}"/> according to specified key selector and element selector functions.
    /// <para />
    /// Explicitly checks for duplicate keys.
    /// <para />
    /// If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If failure, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<Dictionary<TKey, TElement>> TryToDictionary<TSource, TKey, TElement>(this Result<IEnumerable<TSource>> source,
                                                                                              Func<TSource, TKey> keySelector,
                                                                                              Func<TSource, TElement> elementSelector)
        where TKey : notnull =>
        source.Then(x => TryToDictionary(x, keySelector, elementSelector));

    /// <summary>
    /// Try to create a <see cref="Dictionary{TKey, TElement}"/> from an <see cref="IEnumerable{T}"/> according to specified key selector and element selector functions.
    /// <para />
    /// Explicitly checks for duplicate keys.
    /// <para />
    /// If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If failure, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<Dictionary<TKey, TElement>> TryToDictionary<TOriginalKey, TOriginalElement, TKey, TElement>(this Result<IEnumerable<Dictionary<TOriginalKey, TOriginalElement>>> source,
                                                                                                                     Func<KeyValuePair<TOriginalKey, TOriginalElement>, TKey> keySelector,
                                                                                                                     Func<KeyValuePair<TOriginalKey, TOriginalElement>, TElement> elementSelector)
        where TKey : notnull
        where TOriginalKey : notnull =>
        source.FlatMap(dictionary => dictionary.AsEnumerable())
              .Then(keyValuePairs => TryToDictionary(keyValuePairs, keySelector, elementSelector));

    /// <summary>
    /// Try to create a <see cref="Dictionary{TKey, TElement}"/> from an <see cref="IEnumerable{T}"/> according to specified key selector and element selector functions.
    /// <para />
    /// Explicitly checks for duplicate keys.
    /// <para />
    /// If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If successful, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If failure, returns <see cref="Result"/> <see cref="Dictionary{TKey, TElement}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<Dictionary<TKey, TElement>> TryToDictionary<TOriginalKey, TOriginalElement, TKey, TElement>(
        this Result<IEnumerable<IReadOnlyDictionary<TOriginalKey, TOriginalElement>>> source,
        Func<KeyValuePair<TOriginalKey, TOriginalElement>, TKey> keySelector,
        Func<KeyValuePair<TOriginalKey, TOriginalElement>, TElement> elementSelector)
        where TKey : notnull
        where TOriginalKey : notnull =>
        source.FlatMap(dictionary => dictionary.AsEnumerable())
              .Then(keyValuePairs => TryToDictionary(keyValuePairs, keySelector, elementSelector));
}