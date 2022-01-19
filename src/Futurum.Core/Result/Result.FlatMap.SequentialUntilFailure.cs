namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/>, returning the first error it finds.
    /// <para>For each element in <paramref name="source"/> it calls <paramref name="func"/>.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result"/> with <see cref="Result.IsFailure"/> true and with the error.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Result FlatMapSequentialUntilFailure<T>(this IEnumerable<T> source, Func<T, Result> func)
    {
        foreach (var x in source)
        {
            var result = func(x);

            if (result.IsFailure) return result;
        }

        return Result.Ok();
    }

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/>, returning the first error it finds.
    /// <para>For each element in <paramref name="source"/> it calls <paramref name="func"/>.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result"/> with <see cref="Result.IsFailure"/> true and with the error.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static async Task<Result> FlatMapSequentialUntilFailureAsync<T>(this IEnumerable<T> source, Func<T, Task<Result>> func)
    {
        foreach (var x in source)
        {
            var result = await func(x);

            if (result.IsFailure) return result;
        }

        return Result.Ok();
    }
}