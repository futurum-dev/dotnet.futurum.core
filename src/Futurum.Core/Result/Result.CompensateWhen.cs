namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result CompensateWhen<TResultError>(Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Ok();

        return Error.Value switch
        {
            TResultError resultError => compensatingFunc(resultError),
            _                        => Fail(Error.Value)
        };
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result CompensateWhen<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Ok();

        return Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
            _                                                     => Fail(Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> CompensateWhenAsync<TResultError>(Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return OkAsync();

        return Error.Value switch
        {
            TResultError resultError => compensatingFunc(resultError),
            _                        => FailAsync(Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> CompensateWhenAsync<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return OkAsync();

        return Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
            _                                                     => FailAsync(Error.Value)
        };
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> CompensateWhen<TResultError>(Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.Ok(Value.Value);

        return Error.Value switch
        {
            TResultError resultError => compensatingFunc(resultError),
            _                        => Result.Fail<T>(Error.Value)
        };
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> CompensateWhen<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.Ok(Value.Value);

        return Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
            _                                                     => Result.Fail<T>(Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> CompensateWhenAsync<TResultError>(Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.OkAsync(Value.Value);

        return Error.Value switch
        {
            TResultError resultError => compensatingFunc(resultError),
            _                        => Result.FailAsync<T>(Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> CompensateWhenAsync<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.OkAsync(Value.Value);

        return Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
            _                                                     => Result.FailAsync<T>(Error.Value)
        };
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        return result.Error.Value switch
        {
            TResultError resultError => await compensatingFunc(resultError),
            _                        => await Result.FailAsync(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        return result.Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => await compensatingFunc(resultError),
            _                                                     => await Result.FailAsync(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        return result.Error.Value switch
        {
            TResultError resultError => await compensatingFunc(resultError),
            _                        => await Result.FailAsync<T>(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        return result.Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => await compensatingFunc(resultError),
            _                                                     => await Result.FailAsync<T>(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        return result.Error.Value switch
        {
            TResultError resultError => compensatingFunc(resultError),
            _                        => await Result.FailAsync(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, bool> filterFunc, Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        return result.Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
            _                                                     => await Result.FailAsync(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        return result.Error.Value switch
        {
            TResultError resultError => compensatingFunc(resultError),
            _                        => await Result.FailAsync<T>(result.Error.Value)
        };
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, bool> filterFunc, Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        return result.Error.Value switch
        {
            TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
            _                                                     => await Result.FailAsync<T>(result.Error.Value)
        };
    }
}