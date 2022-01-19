using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result" /> <see cref="Result.IsSuccess"/> or <see cref="Result.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public Result DoSwitch(Action successFunc, Action failureFunc)
    {
        if (IsSuccess)
            successFunc();
        else
            failureFunc();

        return this;
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result" /> <see cref="Result.IsSuccess"/> or <see cref="Result.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public Result DoSwitch(Action successFunc, Action<IResultError> failureFunc)
    {
        if (IsSuccess)
            successFunc();
        else
            failureFunc(Error.Value);

        return this;
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result" /> <see cref="Result.IsSuccess"/> or <see cref="Result.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public async Task<Result> DoSwitchAsync(Func<Task> successFunc, Func<Task> failureFunc)
    {
        if (IsSuccess)
            await successFunc();
        else
            await failureFunc();

        return this;
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result" /> <see cref="Result.IsSuccess"/> or <see cref="Result.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public async Task<Result> DoSwitchAsync(Func<Task> successFunc, Func<IResultError, Task> failureFunc)
    {
        if (IsSuccess)
            await successFunc();
        else
            await failureFunc(Error.Value);

        return this;
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> or <see cref="Result{T}.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public Result<T> DoSwitch(Action<T> successFunc, Action failureFunc)
    {
        if (IsSuccess)
            successFunc(Value.Value);
        else
            failureFunc();

        return this;
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> or <see cref="Result{T}.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public Result<T> DoSwitch(Action<T> successFunc, Action<IResultError> failureFunc)
    {
        if (IsSuccess)
            successFunc(Value.Value);
        else
            failureFunc(Error.Value);

        return this;
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> or <see cref="Result{T}.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public async Task<Result<T>> DoSwitchAsync(Func<T, Task> successFunc, Func<Task> failureFunc)
    {
        if (IsSuccess)
            await successFunc(Value.Value);
        else
            await failureFunc();

        return this;
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> or <see cref="Result{T}.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public async Task<Result<T>> DoSwitchAsync(Func<T, Task> successFunc, Func<IResultError, Task> failureFunc)
    {
        if (IsSuccess)
            await successFunc(Value.Value);
        else
            await failureFunc(Error.Value);

        return this;
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result" /> <see cref="Result.IsSuccess"/> or <see cref="Result.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static Task<Result> DoSwitchAsync(this Task<Result> resultTask, Action successFunc, Action failureFunc)
    {
        Result Execute(Result result) => result.DoSwitch(successFunc, failureFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result" /> <see cref="Result.IsSuccess"/> or <see cref="Result.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static Task<Result> DoSwitchAsync(this Task<Result> resultTask, Action successFunc, Action<IResultError> failureFunc)
    {
        Result Execute(Result result) => result.DoSwitch(successFunc, failureFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> or <see cref="Result{T}.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static Task<Result<T>> DoSwitchAsync<T>(this Task<Result<T>> resultTask, Action<T> successFunc, Action failureFunc)
    {
        Result<T> Execute(Result<T> result) => result.DoSwitch(successFunc, failureFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Execute a side-effect, based on if a <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> or <see cref="Result{T}.IsFailure"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="successFunc"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="failureFunc"/>.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoSwitchAsync<T>(this Task<Result<T>> resultTask, Action<T> successFunc, Action<IResultError> failureFunc)
    {
        var result = await resultTask;

        if (result.IsSuccess)
            successFunc(result.Value.Value);
        else
            failureFunc(result.Error.Value);

        return result;
    }
}