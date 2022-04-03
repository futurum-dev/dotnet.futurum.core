namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public Result DoWhenFailure(Action sideEffect)
    {
        if (!IsSuccess)
            sideEffect();

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public Result DoWhenFailure(Action<IResultError> sideEffect)
    {
        if (!IsSuccess)
            sideEffect(Error.Value);

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public async Task<Result> DoWhenFailureAsync(Func<Task> sideEffect)
    {
        if (!IsSuccess)
            await sideEffect();

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public async Task<Result> DoWhenFailureAsync(Func<IResultError, Task> sideEffect)
    {
        if (!IsSuccess)
            await sideEffect(Error.Value);

        return this;
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public Result<T> DoWhenFailure(Action sideEffect)
    {
        if (!IsSuccess)
            sideEffect();

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public Result<T> DoWhenFailure(Action<IResultError> sideEffect)
    {
        if (!IsSuccess)
            sideEffect(Error.Value);

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public async Task<Result<T>> DoWhenFailureAsync(Func<Task> sideEffect)
    {
        if (!IsSuccess)
            await sideEffect();

        return this;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public async Task<Result<T>> DoWhenFailureAsync(Func<IResultError, Task> sideEffect)
    {
        if (!IsSuccess)
            await sideEffect(Error.Value);

        return this;
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoWhenFailureAsync<T>(this Task<Result<T>> resultTask, Func<Task> sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            await sideEffect();

        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static async Task<Result> DoWhenFailureAsync(this Task<Result> resultTask, Func<Task> sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            await sideEffect();
        
        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoWhenFailureAsync<T>(this Task<Result<T>> resultTask, Func<IResultError, Task> sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            await sideEffect(result.Error.Value);
        
        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static async Task<Result> DoWhenFailureAsync(this Task<Result> resultTask, Func<IResultError, Task> sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            await sideEffect(result.Error.Value);
        
        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoWhenFailureAsync<T>(this Task<Result<T>> resultTask, Action sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            sideEffect();

        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static async Task<Result> DoWhenFailureAsync(this Task<Result> resultTask, Action sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            sideEffect();

        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result{T}" /> is returned unchanged.
    /// </summary>
    public static async Task<Result<T>> DoWhenFailureAsync<T>(this Task<Result<T>> resultTask, Action<IResultError> sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            sideEffect(result.Error.Value);

        return result;
    }

    /// <summary>
    /// Execute a side-effect on a failed <see cref="Result" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then call <paramref name="sideEffect"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then do nothing.</description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// The original <see cref="Result" /> is returned unchanged.
    /// </summary>
    public static async Task<Result> DoWhenFailureAsync(this Task<Result> resultTask, Action<IResultError> sideEffect)
    {
        var result = await resultTask;

        if (!result.IsSuccess)
            sideEffect(result.Error.Value);

        return result;
    }
}