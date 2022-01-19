using System.Collections.Concurrent;

namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="source"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsSuccess"/> true and with the transformed values.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsFailure"/> true and with a <see cref="ResultErrorComposite"/> of all the errors.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Control the parallelism with <paramref name="parallelOptions"/> parameter</para>
    /// </summary>
    public static async Task<Result> FlatMapAsync<T>(this IEnumerable<T> source, ParallelOptions parallelOptions, Func<T, Task<Result>> func)
    {
        var concurrentBag = new ConcurrentBag<Result>();

        await Parallel.ForEachAsync(source, parallelOptions,
                                    async (x, _) =>
                                    {
                                        var result = await func(x);

                                        concurrentBag.Add(result);
                                    });

        return concurrentBag.Combine();
    }

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="source"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsSuccess"/> true and with the transformed values.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsFailure"/> true and with a <see cref="ResultErrorComposite"/> of all the errors.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Control the parallelism with <paramref name="parallelOptions"/> parameter</para>
    /// </summary>
    public static async Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this IEnumerable<T> source, ParallelOptions parallelOptions, Func<T, Task<Result<TR>>> func)
    {
        var concurrentBag = new ConcurrentBag<Result<TR>>();

        await Parallel.ForEachAsync(source, parallelOptions,
                                    async (x, _) =>
                                    {
                                        var result = await func(x);

                                        concurrentBag.Add(result);
                                    });

        return concurrentBag.Combine();
    }

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="source"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsSuccess"/> true and with the transformed values.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsFailure"/> true and with a <see cref="ResultErrorComposite"/> of all the errors.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Control the parallelism with <paramref name="parallelOptions"/> parameter</para>
    /// </summary>
    public static async Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this IEnumerable<T> source, ParallelOptions parallelOptions, Func<T, Task<Result<IEnumerable<TR>>>> func)
    {
        var concurrentBag = new ConcurrentBag<Result<IEnumerable<TR>>>();

        await Parallel.ForEachAsync(source, parallelOptions,
                                    async (x, _) =>
                                    {
                                        var result = await func(x);

                                        concurrentBag.Add(result);
                                    });

        return concurrentBag.Combine();
    }

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="source"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsSuccess"/> true and with the transformed values.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsFailure"/> true and with a <see cref="ResultErrorComposite"/> of all the errors.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Control the parallelism with <paramref name="parallelOptions"/> parameter</para>
    /// </summary>
    public static async Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this IEnumerable<T> source, ParallelOptions parallelOptions, Func<T, Task<Result<List<TR>>>> func)
    {
        var concurrentBag = new ConcurrentBag<Result<List<TR>>>();

        await Parallel.ForEachAsync(source, parallelOptions,
                                    async (x, _) =>
                                    {
                                        var result = await func(x);

                                        concurrentBag.Add(result);
                                    });

        return concurrentBag.Combine();
    }

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="resultTaskSource"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsSuccess"/> true and with the transformed values.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsFailure"/> true and with a <see cref="ResultErrorComposite"/> of all the errors.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Control the parallelism with <paramref name="parallelOptions"/> parameter</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultTaskSource, ParallelOptions parallelOptions,
                                                                    Func<T, Task<Result<IEnumerable<TR>>>> func) =>
        resultTaskSource.FlatMapAsync(parallelOptions, func, Combine);

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="resultTaskSource"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all the element transforms are successful, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsSuccess"/> true and with the transformed values.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any of the element transforms fail, then returns <see cref="Result{TR}"/> with <see cref="Result{T}.IsFailure"/> true and with a <see cref="ResultErrorComposite"/> of all the errors.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para>Control the parallelism with <paramref name="parallelOptions"/> parameter</para>
    /// </summary>
    public static Task<Result> FlatMapAsync<T>(this Task<Result<IEnumerable<T>>> resultTaskSource, ParallelOptions parallelOptions, Func<T, Task<Result>> func) =>
        resultTaskSource.FlatMapAsync(parallelOptions, func, Combine);

    public static Task<Result<TR>> FlatMapAsync<T, TI, TR>(this Task<Result<IEnumerable<T>>> resultTaskSource, ParallelOptions parallelOptions,
                                                           Func<T, Task<Result<TI>>> func,
                                                           Func<IEnumerable<Result<TI>>, Result<TR>> reduce)
    {
        async Task<Result<TR>> Execute(IEnumerable<T> source)
        {
            var concurrentBag = new ConcurrentBag<Result<TI>>();

            await Parallel.ForEachAsync(source, parallelOptions,
                                        async (x, _) =>
                                        {
                                            var result = await func(x);

                                            concurrentBag.Add(result);
                                        });

            return reduce(concurrentBag);
        }

        return resultTaskSource.ThenAsync(Execute);
    }

    public static Task<Result> FlatMapAsync<T>(this Task<Result<IEnumerable<T>>> resultTaskSource, ParallelOptions parallelOptions,
                                               Func<T, Task<Result>> func,
                                               Func<IEnumerable<Result>, Result> reduce)
    {
        async Task<Result> Execute(IEnumerable<T> source)
        {
            var concurrentBag = new ConcurrentBag<Result>();

            await Parallel.ForEachAsync(source, parallelOptions,
                                        async (x, _) =>
                                        {
                                            var result = await func(x);

                                            concurrentBag.Add(result);
                                        });

            return reduce(concurrentBag);
        }

        return resultTaskSource.ThenAsync(Execute)
                               .ToNonGenericAsync();
    }
}