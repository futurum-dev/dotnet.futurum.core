using Futurum.Core.Linq;

namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="resultSource"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
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
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMap<T, TR>(this Result<IEnumerable<T>> resultSource, Func<T, IEnumerable<TR>> func) =>
        resultSource.Map(x => x.SelectMany(func));

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
    /// </summary>
    public static Result FlatMap<T>(this IEnumerable<T> source, Func<T, Result> func) =>
        source.Select(func)
              .Combine();

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
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMap<T, TR>(this IEnumerable<T> source, Func<T, Result<TR>> func) =>
        source.Select(func)
              .Combine();

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
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMap<T, TR>(this IEnumerable<T> source, Func<T, Result<IEnumerable<TR>>> func) =>
        source.Select(func)
              .Combine();

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
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMap<T, TR>(this IEnumerable<T> source, Func<T, Result<List<TR>>> func) =>
        source.Select(func)
              .Combine();

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="resultSource"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
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
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMap<T, TR>(this Result<IEnumerable<T>> resultSource, Func<T, Result<IEnumerable<TR>>> func) =>
        resultSource.FlatMap(func, Combine);

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="resultSource"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
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
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMap<T, TR>(this Result<IEnumerable<T>> resultSource, Func<T, Result<TR>> func) =>
        resultSource.Then(x => x.FlatMap(func));

    /// <summary>
    /// Transforms each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
    /// <para>For each element in <paramref name="resultSourceTask"/> it calls <paramref name="func"/>, the result <see cref="IEnumerable{T}"/> of <see cref="Result{TR}"/> are the combined.</para>
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
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultSourceTask, Func<T, Result<TR>> func) =>
        resultSourceTask.ThenAsync(x => x.FlatMap(func));

    /// <summary>
    /// Flattens each element of a sequence of sequence into one sequence.
    /// </summary>
    public static Result<IEnumerable<T>> FlatMap<T>(this Result<IEnumerable<IEnumerable<T>>> resultSource) =>
        resultSource.Map(EnumerableExtensions.SelectMany);

    /// <summary>
    /// Flattens each element of a sequence of sequence into one sequence.
    /// </summary>
    public static Result<IEnumerable<T>> FlatMap<T>(this Result<IEnumerable<List<T>>> resultSource) =>
        resultSource.Map(EnumerableExtensions.SelectMany);

    public static Result<TR> FlatMap<T, TI, TR>(this Result<IEnumerable<T>> resultSource, Func<T, Result<TI>> func, Func<IEnumerable<Result<TI>>, Result<TR>> reduce)
    {
        Result<TR> Execute(IEnumerable<T> source)
        {
            var values = source.Select(func);
            
            return reduce(values);
        }

        return resultSource.Then(Execute);
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
    /// <para>Runs with uncontrolled parallelism</para>
    /// </summary>
    public static Task<Result> FlatMapAsync<T>(this IEnumerable<T> source, Func<T, Task<Result>> func) =>
        source.FlatMapAsync(DefaultParallelOptions, func);

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
    /// <para>Runs with uncontrolled parallelism</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this IEnumerable<T> source, Func<T, Task<Result<TR>>> func) =>
        source.FlatMapAsync(DefaultParallelOptions, func);

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
    /// <para>Runs with uncontrolled parallelism</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this IEnumerable<T> source, Func<T, Task<Result<IEnumerable<TR>>>> func) =>
        source.FlatMapAsync(DefaultParallelOptions, func);

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
    /// <para>Runs with uncontrolled parallelism</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this IEnumerable<T> source, Func<T, Task<Result<List<TR>>>> func) =>
        source.FlatMapAsync(DefaultParallelOptions, func);

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
    /// <para>Runs with uncontrolled parallelism</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultTaskSource, Func<T, Task<Result<IEnumerable<TR>>>> func) =>
        resultTaskSource.FlatMapAsync(DefaultParallelOptions, func);

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
    /// <para>Runs with uncontrolled parallelism</para>
    /// </summary>
    public static Task<Result> FlatMapAsync<T>(this Task<Result<IEnumerable<T>>> resultTaskSource, Func<T, Task<Result>> func) =>
        resultTaskSource.FlatMapAsync(DefaultParallelOptions, func);
    
    private static readonly ParallelOptions DefaultParallelOptions = new();
}