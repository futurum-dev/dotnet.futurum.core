namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result CompensateWhenAny<TResultError>(Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Ok();

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Result CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Fail(error)
            };
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result CompensateWhenAny<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Ok();

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultErrors = resultErrorComposite.Flatten();
            var resultError = resultErrors
                              .OfType<TResultError>()
                              .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Result CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Fail(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> CompensateWhenAnyAsync<TResultError>(Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return OkAsync();

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Task<Result> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => FailAsync(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result> CompensateWhenAnyAsync<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return OkAsync();

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Task<Result> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => FailAsync(error)
            };
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> CompensateWhenAny<TResultError>(Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.Ok(Value.Value);

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Result<T> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Result.Fail<T>(error)
            };
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Result<T> CompensateWhenAny<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.Ok(Value.Value);

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Result<T> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Result.Fail<T>(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> CompensateWhenAnyAsync<TResultError>(Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.OkAsync(Value.Value);

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Task<Result<T>> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Result.FailAsync<T>(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public Task<Result<T>> CompensateWhenAnyAsync<TResultError>(Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        if (IsSuccess)
            return Result.OkAsync(Value.Value);

        if (Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(Error.Value);

        Task<Result<T>> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Result.FailAsync<T>(error)
            };
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAnyAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return await CompensatingFunc(resultError);
            }
        }

        return await CompensatingFunc(result.Error.Value);

        Task<Result> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Result.FailAsync(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAnyAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, bool> filterFunc, Func<TResultError, Task<Result>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return await CompensatingFunc(resultError);
            }
        }

        return await CompensatingFunc(result.Error.Value);

        Task<Result> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Result.FailAsync(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAnyAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return await CompensatingFunc(resultError);
            }
        }

        return await CompensatingFunc(result.Error.Value);

        Task<Result<T>> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Result.FailAsync<T>(error)
            };
    }

    /// <summary>
    /// Allows a different async <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAnyAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, bool> filterFunc,
                                                                                Func<TResultError, Task<Result<T>>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return await CompensatingFunc(resultError);
            }
        }

        return await CompensatingFunc(result.Error.Value);

        Task<Result<T>> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Result.FailAsync<T>(error)
            };
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAnyAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(result.Error.Value);

        Result CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Result.Fail(error)
            };
    }

    /// <summary>
    /// Allows a different <see cref="Result" /> to be provided in the case of a failed async <see cref="Result" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsFailure"/> is true, then return a compensating <see cref="Result" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result" /> <see cref="Result.IsSuccess"/> is true, then return the original <see cref="Result" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> CompensateWhenAnyAsync<TResultError>(this Task<Result> resultTask, Func<TResultError, bool> filterFunc, Func<TResultError, Result> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync();

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(result.Error.Value);

        Result CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Result.Fail(error)
            };
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/>.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAnyAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(result.Error.Value);

        Result<T> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError => compensatingFunc(resultError),
                _                        => Result.Fail<T>(error)
            };
    }

    /// <summary>
    /// Allows a different <see cref="Result{T}" /> to be provided in the case of a failed async <see cref="Result{T}" />,
    /// when the <see cref="IResultError"/> is of type <typeparamref name="TResultError"/> and <paramref name="filterFunc"/> returns true.
    /// This will flatten a <see cref="IResultErrorComposite"/> and check each <see cref="IResultError"/> in the composite.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsFailure"/> is true, then return a compensating <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>If an <see cref="Result{T}" /> <see cref="Result{T}.IsSuccess"/> is true, then return the original <see cref="Result{T}" />.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> CompensateWhenAnyAsync<T, TResultError>(this Task<Result<T>> resultTask, Func<TResultError, bool> filterFunc,
                                                                                Func<TResultError, Result<T>> compensatingFunc)
        where TResultError : IResultError
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return await Result.OkAsync(result.Value.Value);

        if (result.Error.Value is IResultErrorComposite resultErrorComposite)
        {
            var resultError = resultErrorComposite.Flatten()
                                                  .OfType<TResultError>()
                                                  .FirstOrDefault();

            if (resultError is not null)
            {
                return CompensatingFunc(resultError);
            }
        }

        return CompensatingFunc(result.Error.Value);

        Result<T> CompensatingFunc(IResultError error) =>
            error switch
            {
                TResultError resultError when filterFunc(resultError) => compensatingFunc(resultError),
                _                                                     => Result.Fail<T>(error)
            };
    }
}