namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="T"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public T Switch<T>(Func<T> successFunc, Func<T> failureFunc) =>
        IsSuccess ? successFunc() : failureFunc();

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="T"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public T Switch<T>(Func<T> successFunc, Func<IResultError, T> failureFunc) =>
        IsSuccess ? successFunc() : failureFunc(Error.Value);

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="T"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<T> SwitchAsync<T>(Func<Task<T>> successFunc, Func<Task<T>> failureFunc) =>
        IsSuccess ? successFunc() : failureFunc();

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="T"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<T> SwitchAsync<T>(Func<Task<T>> successFunc, Func<IResultError, Task<T>> failureFunc) =>
        IsSuccess ? successFunc() : failureFunc(Error.Value);
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public TR Switch<TR>(Func<T, TR> successFunc, Func<TR> failureFunc) =>
        IsSuccess ? successFunc(Value.Value) : failureFunc();

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public TR Switch<TR>(Func<T, TR> successFunc, Func<IResultError, TR> failureFunc) =>
        IsSuccess ? successFunc(Value.Value) : failureFunc(Error.Value);

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<TR> SwitchAsync<TR>(Func<T, Task<TR>> successFunc, Func<Task<TR>> failureFunc) =>
        IsSuccess ? successFunc(Value.Value) : failureFunc();

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<TR> SwitchAsync<TR>(Func<T, Task<TR>> successFunc, Func<IResultError, Task<TR>> failureFunc) =>
        IsSuccess ? successFunc(Value.Value) : failureFunc(Error.Value);
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="T"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<T> SwitchAsync<T>(this Task<Result> resultTask, Func<T> successFunc, Func<T> failureFunc)
    {
        var result = await resultTask;

        return result.IsSuccess ? successFunc() : failureFunc();
    }

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="T"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<T> SwitchAsync<T>(this Task<Result> resultTask, Func<T> successFunc, Func<IResultError, T> failureFunc)
    {
        var result = await resultTask;

        return result.IsSuccess ? successFunc() : failureFunc(result.Error.Value);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<TR> SwitchAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, TR> successFunc, Func<TR> failureFunc)
    {
        var result = await resultTask;

        return result.IsSuccess ? successFunc(result.Value.Value) : failureFunc();
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<TR> SwitchAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, TR> successFunc, Func<IResultError, TR> failureFunc)
    {
        var result = await resultTask;

        return result.IsSuccess ? successFunc(result.Value.Value) : failureFunc(result.Error.Value);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<TR> SwitchAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Task<TR>> successFunc, Func<IResultError, Task<TR>> failureFunc)
    {
        var result = await resultTask;

        return result.IsSuccess ? await successFunc(result.Value.Value) : await failureFunc(result.Error.Value);
    }
}