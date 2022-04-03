namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Execute a side-effect on a successful <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public Result Do(Action sideEffect)
    {
        if (IsSuccess) sideEffect();

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a successful <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original async <see cref="Result" /> is returned unchanged.
    /// </summary>
    public async Task<Result> DoAsync(Func<Task> sideEffect)
    {
        if (IsSuccess) await sideEffect();

        return this;
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Execute a side-effect on a successful <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public Result<T> Do(Action<T> sideEffect)
    {
        if (IsSuccess) sideEffect(Value.Value);

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a successful <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original async <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public async Task<Result<T>> DoAsync(Func<T, Task> sideEffect)
    {
        if (IsSuccess) await sideEffect(Value.Value);

        return this;
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Execute a side-effect on a successful async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an async <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an async <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original async <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoAsync<T>(this Task<Result<T>> resultTask, Func<T, Task> sideEffect)
    {
        var result = await resultTask;

        if (result.IsSuccess) await sideEffect(result.Value.Value);
        
        return result;
    }

    /// <summary>
    /// Execute a side-effect on a successful async <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an async <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an async <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original async <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static async Task<Result> DoAsync(this Task<Result> resultTask, Func<Task> sideEffect)
    {
        var result = await resultTask;

        if (result.IsSuccess) await sideEffect();
        
        return result;
    }

    /// <summary>
    /// Execute a side-effect on a successful async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an async <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an async <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original async <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoAsync<T>(this Task<Result<T>> resultTask, Action<T> sideEffect)
    {
        var result = await resultTask;

        if (result.IsSuccess) sideEffect(result.Value.Value);

        return result;
    }

    /// <summary>
    /// Execute a side-effect on a successful async <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an async <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an async <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original async <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result> DoAsync(this Task<Result> resultTask, Action sideEffect)
    {
        var result = await resultTask;

        if (result.IsSuccess) sideEffect();

        return result;
    }
}