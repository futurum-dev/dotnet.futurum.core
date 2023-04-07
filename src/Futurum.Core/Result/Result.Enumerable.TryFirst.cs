using Futurum.Core.Option;

namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Returns the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// Each element is sequentially passed to <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true, then the element is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsFailure"/> true, then the element is ignored. and the next element is passed to <paramref name="func"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// If none of the elements in the sequence when passed to <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true,
    /// then <see cref="Result"/> with <see cref="Result.IsFailure"/> true and <paramref name="errorMessage"/> as the error is returned.
    /// </summary>
    public static Result<TR> TryFirst<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Result<TR>> func, string errorMessage)
    {
        return Run(Option<Result<TR>>.None, source.ToArray());

        Result<TR> Run(Option<Result<TR>> previous, TSource[] items) =>
            items.Length switch
            {
                0 => Result.Fail<TR>(errorMessage),
                _ => previous.HasNoValue
                    ? NoPrevious(items[0], items[1..])
                    : WithPrevious(previous.Value, items[0], items[1..])
            };

        Result<TR> NoPrevious(TSource item, TSource[] items)
        {
            var current = func(item);

            return current.IsSuccess
                ? Result.Ok(current.Value.Value)
                : Run(current, items);
        }

        Result<TR> WithPrevious(Result<TR> previous, TSource item, TSource[] items)
        {
            var current = previous.IsSuccess
                ? Result.Ok(previous.Value.Value)
                : func(item);

            return current.IsSuccess
                ? Result.Ok(current.Value.Value)
                : Run(current, items);
        }
    }

    /// <summary>
    /// Returns the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// Each element is sequentially passed to <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true, then the element is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsFailure"/> true, then the element is ignored. and the next element is passed to <paramref name="func"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// If none of the elements in the sequence when passed to <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true,
    /// then <see cref="Result"/> with <see cref="Result.IsFailure"/> true and <paramref name="errorMessage"/> as the error is returned.
    /// </summary>
    public static Result<TR> TryFirst<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Result<TR>> func, IResultError errorMessage)
    {
        return Run(Option<Result<TR>>.None, source.ToArray());

        Result<TR> Run(Option<Result<TR>> previous, TSource[] items) =>
            items.Length switch
            {
                0 => Result.Fail<TR>(errorMessage),
                _ => previous.HasNoValue
                    ? NoPrevious(items[0], items[1..])
                    : WithPrevious(previous.Value, items[0], items[1..])
            };

        Result<TR> NoPrevious(TSource item, TSource[] items)
        {
            var current = func(item);

            return current.IsSuccess
                ? Result.Ok(current.Value.Value)
                : Run(current, items);
        }

        Result<TR> WithPrevious(Result<TR> previous, TSource item, TSource[] items)
        {
            var current = previous.IsSuccess
                ? Result.Ok(previous.Value.Value)
                : func(item);

            return current.IsSuccess
                ? Result.Ok(current.Value.Value)
                : Run(current, items);
        }
    }

    /// <summary>
    /// Returns the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// Each element is sequentially passed to <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true, then the element is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsFailure"/> true, then the element is ignored. and the next element is passed to <paramref name="func"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// If none of the elements in the sequence when passed to <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true,
    /// then <see cref="Result"/> with <see cref="Result.IsFailure"/> true and <paramref name="errorMessage"/> as the error is returned.
    /// </summary>
    public static Task<Result<TR>> TryFirstAsync<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Task<Result<TR>>> func, string errorMessage)
    {
        return RunAsync(Option<Result<TR>>.None, source.ToArray());

        async Task<Result<TR>> RunAsync(Option<Result<TR>> previous, TSource[] items) =>
            items.Length switch
            {
                0 => Result.Fail<TR>(errorMessage),
                _ => previous.HasNoValue
                    ? await NoPrevious(items[0], items[1..])
                    : await WithPrevious(previous.Value, items[0], items[1..])
            };

        async Task<Result<TR>> NoPrevious(TSource item, TSource[] items)
        {
            var current = await func(item);

            return await (current.IsSuccess
                ? Result.OkAsync(current.Value.Value)
                : RunAsync(current, items));
        }

        async Task<Result<TR>> WithPrevious(Result<TR> previous, TSource item, TSource[] items)
        {
            var current = await (previous.IsSuccess
                ? Result.OkAsync(previous.Value.Value)
                : func(item));

            return await (current.IsSuccess
                ? Result.OkAsync(current.Value.Value)
                : RunAsync(current, items));
        }
    }

    /// <summary>
    /// Returns the first element of a sequence based on <see cref="Result.FilterSuccess"/> predicate.
    /// <para />
    /// Each element is sequentially passed to <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true, then the element is returned.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsFailure"/> true, then the element is ignored. and the next element is passed to <paramref name="func"/>.
    ///         </description>
    ///     </item>
    /// </list>
    /// If none of the elements in the sequence when passed to <paramref name="func"/> returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true,
    /// then <see cref="Result"/> with <see cref="Result.IsFailure"/> true and <paramref name="errorMessage"/> as the error is returned.
    /// </summary>
    public static Task<Result<TR>> TryFirstAsync<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Task<Result<TR>>> func, IResultError errorMessage)
    {
        return RunAsync(Option<Result<TR>>.None, source.ToArray());

        async Task<Result<TR>> RunAsync(Option<Result<TR>> previous, TSource[] items) =>
            items.Length switch
            {
                0 => Result.Fail<TR>(errorMessage),
                _ => previous.HasNoValue
                    ? await NoPrevious(items[0], items[1..])
                    : await WithPrevious(previous.Value, items[0], items[1..])
            };

        async Task<Result<TR>> NoPrevious(TSource item, TSource[] items)
        {
            var current = await func(item);

            return await (current.IsSuccess
                ? Result.OkAsync(current.Value.Value)
                : RunAsync(current, items));
        }

        async Task<Result<TR>> WithPrevious(Result<TR> previous, TSource item, TSource[] items)
        {
            var current = await (previous.IsSuccess
                ? Result.OkAsync(previous.Value.Value)
                : func(item));

            return await (current.IsSuccess
                ? Result.OkAsync(current.Value.Value)
                : RunAsync(current, items));
        }
    }
}