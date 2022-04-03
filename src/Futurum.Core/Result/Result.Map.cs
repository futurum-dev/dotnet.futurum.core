namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="Result{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> Map<T>(T value) =>
        IsFailure ? Fail<T>(Error.Value) : value.ToResultOk();

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="Result{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> Map<T>(Func<T> func) =>
        IsFailure ? Fail<T>(Error.Value) : func().ToResultOk();

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="Result{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public async Task<Result<T>> MapAsync<T>(Func<Task<T>> func) =>
        IsFailure ? Fail<T>(Error.Value) : (await func()).ToResultOk();
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result{TR}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{TR}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{TR}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<TR> Map<TR>(Func<T, TR> func) =>
        IsFailure ? Result.Fail<TR>(Error.Value) : func(Value.Value).ToResultOk();

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to async <see cref="Result{TR}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then return an async<see cref="Result{TR}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then return an async failed <see cref="Result{TR}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public async Task<Result<TR>> MapAsync<TR>(Func<T, Task<TR>> func) =>
        IsFailure ? Result.Fail<TR>(Error.Value) : (await func(Value.Value)).ToResultOk();
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms <see cref="Result{T}"/> to async <see cref="Result{TR}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then return an async<see cref="Result{TR}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then return an async failed <see cref="Result{TR}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<TR>> MapAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Task<TR>> func)
    {
        var result = await resultTask;

        return result.IsFailure ? Result.Fail<TR>(result.Error.Value) : (await func(result.Value.Value)).ToResultOk();
    }

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="Result{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> MapAsync<T>(this Task<Result> resultTask, Func<Task<T>> func)
    {
        var result = await resultTask;

        return result.IsFailure ? Result.Fail<T>(result.Error.Value) : (await func()).ToResultOk();
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to async <see cref="Result{TR}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsSuccess"/> is true, then return an async<see cref="Result{TR}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result.IsFailure"/> is true, then return an async failed <see cref="Result{TR}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<TR>> MapAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, TR> func)
    {
        var result = await resultTask;

        return result.IsFailure ? Result.Fail<TR>(result.Error.Value) : func(result.Value.Value).ToResultOk();
    }

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="Result{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> MapAsync<T>(this Task<Result> resultTask, T value)
    {
        var result = await resultTask;

        return result.IsFailure ? Result.Fail<T>(result.Error.Value) : value.ToResultOk();
    }

    /// <summary>
    /// Transforms <see cref="Result"/> to <see cref="Result{T}"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return an failed <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> MapAsync<T>(this Task<Result> resultTask, Func<T> func)
    {
        var value = await resultTask;

        return value.IsFailure ? Result.Fail<T>(value.Error.Value) : func().ToResultOk();
    }

    public static async Task<Result<TR2>> MapAsync<T, TR1, TR2>(this Task<Result<T>> resultTask, Func<T, TR1> selectorFunc, Func<TR1, TR2> func)
    {
        var value = await resultTask;

        return value.IsFailure ? Result.Fail<TR2>(value.Error.Value) : func(selectorFunc(value.Value.Value)).ToResultOk();
    }
}