using Futurum.Core.Linq;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Combines multiple <see cref="Result"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result.IsSuccess"/> true, the return a <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result.IsFailure"/> true, the return a <see cref="Result"/> with <see cref="Result.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result Combine(params Result[] results) =>
        results.Combine();

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<IEnumerable<T>> Combine<T>(params Result<T>[] results) =>
        results.Combine();

    /// <summary>
    /// Combines multiple <see cref="Result"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> CombineAsync(params Task<Result>[] resultTasks) =>
        resultTasks.CombineAsync();

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<IEnumerable<T>>> CombineAsync<T>(params Task<Result<T>>[] resultTasks) =>
        resultTasks.CombineAsync();

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<IEnumerable<T>>> CombineAsync<T>(params Task<Result<IEnumerable<T>>>[] resultTasks) =>
        resultTasks.CombineAsync();

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<IEnumerable<T>>> CombineAsync<T>(params Task<Result<List<T>>>[] resultTasks) =>
        resultTasks.CombineAsync();
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Combines multiple <see cref="Result"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result.IsSuccess"/> true, the return a <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result.IsFailure"/> true, the return a <see cref="Result"/> with <see cref="Result.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result Combine(this IEnumerable<Result> results)
    {
        var (failures, _) = results.Partition(Result.FilterFailure);

        return failures.Any() switch
        {
            false => Result.Ok(),
            true  => failures.Select(GetError).ToResultError().ToFailResult()
        };
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<IEnumerable<T>> Combine<T>(this IEnumerable<Result<T>> results)
    {
        var (failures, _) = results.Partition(Result.FilterFailure);

        return failures.Any() switch
        {
            false => results.Select(GetValue).ToResultOk(),
            true  => failures.Select(GetError).ToResultError().ToFailResult<IEnumerable<T>>()
        };
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<IEnumerable<T>> Combine<T>(this IEnumerable<Result<IEnumerable<T>>> resultSource) =>
        Combine<IEnumerable<T>>(resultSource)
            .FlatMap();

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<IEnumerable<T>> Combine<T>(this IEnumerable<Result<List<T>>> resultSource) =>
        Combine<List<T>>(resultSource)
            .FlatMap();

    /// <summary>
    /// Combines multiple <see cref="Result"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result.IsSuccess"/> true, the return a <see cref="Result"/> with <see cref="Result.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result.IsFailure"/> true, the return a <see cref="Result"/> with <see cref="Result.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CombineAsync(this IEnumerable<Task<Result>> resultTasks)
    {
        var results = await Task.WhenAll(resultTasks);

        return Combine(results);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<IEnumerable<T>>> CombineAsync<T>(this IEnumerable<Task<Result<T>>> resultTasks)
    {
        var results = await Task.WhenAll(resultTasks);

        return Combine(results);
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<IEnumerable<T>>> CombineAsync<T>(this IEnumerable<Task<Result<IEnumerable<T>>>> resultTasks)
    {
        var results = await Task.WhenAll(resultTasks);

        return Combine<IEnumerable<T>>(results).FlatMap();
    }

    /// <summary>
    /// Combines multiple <see cref="Result{T}"/>'s into one.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If all are <see cref="Result{T}.IsSuccess"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If any are <see cref="Result{T}.IsFailure"/> true, the return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<IEnumerable<T>>> CombineAsync<T>(this IEnumerable<Task<Result<List<T>>>> resultTasks)
    {
        var results = await Task.WhenAll(resultTasks);

        return Combine<List<T>>(results).FlatMap();
    }
}