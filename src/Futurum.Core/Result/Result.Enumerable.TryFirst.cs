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
        foreach (var x in source)
        {
            var result = func(x);

            if (result.IsSuccess) return result;
        }

        return Result.Fail<TR>(errorMessage);
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
        foreach (var x in source)
        {
            var result = func(x);

            if (result.IsSuccess) return result;
        }

        return Result.Fail<TR>(errorMessage);
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
    public static async Task<Result<TR>> TryFirstAsync<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Task<Result<TR>>> func, string errorMessage)
    {
        foreach (var x in source)
        {
            var result = await func(x);

            if (result.IsSuccess) return result;
        }

        return Result.Fail<TR>(errorMessage);
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
    public static async Task<Result<TR>> TryFirstAsync<TSource, TR>(this IEnumerable<TSource> source, Func<TSource, Task<Result<TR>>> func, IResultError errorMessage)
    {
        foreach (var x in source)
        {
            var result = await func(x);

            if (result.IsSuccess) return result;
        }

        return Result.Fail<TR>(errorMessage);
    }
}