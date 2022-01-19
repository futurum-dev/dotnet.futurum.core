using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result Compensate(Func<Result> compensatingFunc) =>
        Switch(Ok, compensatingFunc);

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result Compensate(Func<IResultError, Result> compensatingFunc) =>
        Switch(Ok, compensatingFunc);

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> CompensateAsync(Func<Task<Result>> compensatingFunc) =>
        SwitchAsync(OkAsync, compensatingFunc);

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> CompensateAsync(Func<IResultError, Task<Result>> compensatingFunc) =>
        SwitchAsync(OkAsync, compensatingFunc);
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> Compensate(Func<Result<T>> compensatingFunc) =>
        Switch(Result.Ok, compensatingFunc);

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> Compensate(Func<IResultError, Result<T>> compensatingFunc) =>
        Switch(Result.Ok, compensatingFunc);

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> CompensateAsync(Func<Task<Result<T>>> compensatingFunc) =>
        SwitchAsync(Result.OkAsync, compensatingFunc);

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> CompensateAsync(Func<IResultError, Task<Result<T>>> compensatingFunc) =>
        SwitchAsync(Result.OkAsync, compensatingFunc);
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> CompensateAsync<T>(this Task<Result<T>> resultTask, Func<Task<Result<T>>> compensatingFunc)
    {
        Task<Result<T>> Execute(Result<T> result) => result.CompensateAsync(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> CompensateAsync(this Task<Result> resultTask, Func<Task<Result>> compensatingFunc)
    {
        Task<Result> Execute(Result result) => result.CompensateAsync(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> CompensateAsync<T>(this Task<Result<T>> resultTask, Func<IResultError, Task<Result<T>>> compensatingFunc)
    {
        Task<Result<T>> Execute(Result<T> result) => result.CompensateAsync(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> CompensateAsync(this Task<Result> resultTask, Func<IResultError, Task<Result>> compensatingFunc)
    {
        Task<Result> Execute(Result result) => result.CompensateAsync(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> CompensateAsync<T>(this Task<Result<T>> resultTask, Func<Result<T>> compensatingFunc)
    {
        Result<T> Execute(Result<T> result) => result.Compensate(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> CompensateAsync(this Task<Result> resultTask, Func<Result> compensatingFunc)
    {
        Result Execute(Result result) => result.Compensate(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> CompensateAsync<T>(this Task<Result<T>> resultTask, Func<IResultError, Result<T>> compensatingFunc)
    {
        Result<T> Execute(Result<T> result) => result.Compensate(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> CompensateAsync(this Task<Result> resultTask, Func<IResultError, Result> compensatingFunc)
    {
        Result Execute(Result result) => result.Compensate(compensatingFunc);

        return resultTask.PipeAsync(Execute);
    }
}