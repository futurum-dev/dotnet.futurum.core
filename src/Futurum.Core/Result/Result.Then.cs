using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Transforms an <see cref="Result" /> into the next <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a success, then return the next <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a failure, then return an failed <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result Then(Func<Result> nextResult) =>
        Switch(nextResult, Fail);

    /// <summary>
    /// Transforms an <see cref="Result" /> into the next <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a success, then return the next <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a failure, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> Then<T>(Func<Result<T>> nextResult) =>
        Switch(nextResult, Fail<T>);

    /// <summary>
    /// Transforms an <see cref="Result" /> into the next async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a success, then return the next async <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a failure, then return an failed async <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> ThenAsync(Func<Task<Result>> nextResult)
    {
        return Switch(nextResult, FailAsync);
    }

    /// <summary>
    /// Transforms an <see cref="Result" /> into the next async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a success, then return the next async <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> is a failure, then return an failed async <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> ThenAsync<T>(Func<Task<Result<T>>> nextResult) =>
        Switch(nextResult, FailAsync<T>);
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Transforms an <see cref="Result{T}" /> into the next <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an <see cref="Result{T}" /> is a success, then return the next <see cref="Result" /> passing
    ///         the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> is a failure, then return an failed <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> Then(Func<T, Result> nextResult)
    {
        var result = this;

        return Switch(nextResult, Result.Fail)
            .Then(() => result);
    }

    /// <summary>
    /// Transforms an <see cref="Result{T}" /> into the next <see cref="Result{TR}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an <see cref="Result{T}" /> is a success, then return the next <see cref="Result{TR}" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> is a failure, then return an failed <see cref="Result{TR}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<TR> Then<TR>(Func<T, Result<TR>> nextResult) =>
        IsSuccess ? nextResult(Value.Value) : Result.Fail<TR>(Error.Value);

    /// <summary>
    /// Transforms an <see cref="Result{T}" /> into the next async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an <see cref="Result{T}" /> is a success, then return the next async <see cref="Result" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> is a failure, then return an failed async <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public async Task<Result<T>> ThenAsync(Func<T, Task<Result>> nextResult)
    {
        if (IsSuccess)
        {
            var result = await nextResult(Value.Value);

            return result.IsFailure ? Result.Fail<T>(result.Error.Value) : Value.Value.ToResultOk();
        }

        return Result.Fail<T>(Error.Value);
    }

    /// <summary>
    /// Transforms an <see cref="Result{T}" /> into the next async <see cref="Result{TR}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an <see cref="Result{T}" /> is a success, then return the next async <see cref="Result{TR}" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an <see cref="Result{T}" /> is a failure, then return an failed async <see cref="Result{TR}" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<TR>> ThenAsync<TR>(Func<T, Task<Result<TR>>> nextResult) =>
        IsSuccess ? nextResult(Value.Value) : Result.FailAsync<TR>(Error.Value);
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms an async <see cref="Result" /> into the next async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an async <see cref="Result" /> is a success, then return the next async <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an async <see cref="Result" /> is a failure, then return an failed async <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> ThenAsync(this Task<Result> resultTask, Func<Task<Result>> nextResult)
    {
        Task<Result> Execute(Result result) => result.ThenAsync(nextResult);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result" /> into the next async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result" /> is a success, then return the next async <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result" /> is a failure, then return an failed async <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenAsync<T>(this Task<Result> resultTask, Func<Task<Result<T>>> nextResult)
    {
        Task<Result<T>> Execute(Result result) => result.ThenAsync(nextResult);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result{T}" /> into the next async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a success, then return the next async <see cref="Result" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a failure, then return an failed async <see cref="Result" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ThenAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> nextResult)
    {
        var result = await resultTask;

        return result.IsSuccess ? await nextResult(result.Value.Value).MapAsync(result.Value.Value) : await Result.FailAsync<T>(result.Error.Value);
    }

    /// <summary>
    /// Transforms an async <see cref="Result{T}" /> into the next async <see cref="Result{TR}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a success, then return the next async <see cref="Result{TR}" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a failure, then return an failed async <see cref="Result{TR}" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<TR>> ThenAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Task<Result<TR>>> nextResult)
    {
        var result = await resultTask;

        return result.IsSuccess ? await nextResult(result.Value.Value) : await Result.FailAsync<TR>(result.Error.Value);
    }

    /// <summary>
    /// Transforms an async <see cref="Result" /> into the next async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an async <see cref="Result" /> is a success, then return the next async <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an async <see cref="Result" /> is a failure, then return an failed async <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result> ThenAsync(this Task<Result> resultTask, Func<Result> nextResult)
    {
        Result Execute(Result result) => result.Then(nextResult);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result" /> into the next async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result" /> is a success, then return the next async <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result" /> is a failure, then return an failed async <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ThenAsync<T>(this Task<Result> resultTask, Func<Result<T>> nextResult)
    {
        var result = await resultTask;

        return result.IsSuccess ? nextResult() : Result.Fail<T>(result.Error.Value);
    }

    /// <summary>
    /// Transforms an async <see cref="Result{T}" /> into the next async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a success, then return the next async <see cref="Result" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a failure, then return an failed async <see cref="Result" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenAsync<T>(this Task<Result<T>> resultTask, Func<T, Result> nextResult)
    {
        Result<T> Execute(Result<T> result) => result.Then(nextResult);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result{T}" /> into the next async <see cref="Result{TR}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a success, then return the next async <see cref="Result{TR}" /> passing the <see cref="Result{T}" />'s Value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If an async <see cref="Result{T}" /> is a failure, then return an failed async <see cref="Result{TR}" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<TR>> ThenAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Result<TR>> nextResult)
    {
        var result = await resultTask;

        return result.IsSuccess ? nextResult(result.Value.Value) : Result.Fail<TR>(result.Error.Value);
    }
}