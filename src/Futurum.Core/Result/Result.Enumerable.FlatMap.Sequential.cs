namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
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
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Task<Result> FlatMapSequentialAsync<T>(this IEnumerable<T> source, Func<T, Task<Result>> func) =>
        source.FlatMapAsync(SequentialParallelOptions, func);

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
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapSequentialAsync<T, TR>(this IEnumerable<T> source, Func<T, Task<Result<TR>>> func) =>
        source.FlatMapAsync(SequentialParallelOptions, func);

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
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapSequentialAsync<T, TR>(this IEnumerable<T> source, Func<T, Task<Result<IEnumerable<TR>>>> func) =>
        source.FlatMapAsync(SequentialParallelOptions, func);

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
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapSequentialAsync<T, TR>(this IEnumerable<T> source, Func<T, Task<Result<List<TR>>>> func) =>
        source.FlatMapAsync(SequentialParallelOptions, func);

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
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> FlatMapSequentialAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultTaskSource, Func<T, Task<Result<IEnumerable<TR>>>> func) =>
        resultTaskSource.FlatMapAsync(SequentialParallelOptions, func);

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
    /// <para>Runs without parallelism i.e. sequential</para>
    /// </summary>
    public static Task<Result> FlatMapSequentialAsync<T>(this Task<Result<IEnumerable<T>>> resultTaskSource, Func<T, Task<Result>> func) =>
        resultTaskSource.FlatMapAsync(SequentialParallelOptions, func);

    private static readonly ParallelOptions SequentialParallelOptions = new() { MaxDegreeOfParallelism = 1 };
}