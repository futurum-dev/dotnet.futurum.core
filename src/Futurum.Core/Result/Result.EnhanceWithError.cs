using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// If <see cref="Result"/> <see cref="IsFailure"/> is true, then add additional error context
    /// </summary>
    public Result EnhanceWithError(Func<string> errorMessage)
    {
        Result Failure(IResultError resultError) =>
            Fail(resultError.EnhanceWithError(errorMessage().ToResultError()));

        return Compensate(Failure);
    }

    /// <summary>
    /// If <see cref="Result"/> <see cref="IsFailure"/> is true, then add additional error context
    /// </summary>
    public Result EnhanceWithError(IResultErrorNonComposite error)
    {
        Result Failure(IResultError resultError) =>
            Fail(resultError.EnhanceWithError(error));

        return Compensate(Failure);
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// If <see cref="Result{T}"/> <see cref="IsFailure"/> is true, then add additional error context
    /// </summary>
    public Result<T> EnhanceWithError(Func<string> errorMessage)
    {
        Result<T> Failure(IResultError resultError) =>
            Result.Fail<T>(resultError.EnhanceWithError(errorMessage()));

        return Compensate(Failure);
    }

    /// <summary>
    /// If <see cref="Result{T}"/> <see cref="IsFailure"/> is true, then add additional error context
    /// </summary>
    public Result<T> EnhanceWithError(IResultErrorNonComposite error)
    {
        Result<T> Failure(IResultError resultError) =>
            Result.Fail<T>(resultError.EnhanceWithError(error));

        return Compensate(Failure);
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Create a composite <see cref="IResultError"/> with <paramref name="errorMessage"/> as the parent 
    /// </summary>
    public static IResultError EnhanceWithError(this IResultError resultError, string errorMessage) =>
        ResultErrorCompositeExtensions.ToResultError(errorMessage.ToResultError(), resultError);

    /// <summary>
    /// Create a composite <see cref="IResultError"/> with <paramref name="error"/> as the parent 
    /// </summary>
    public static IResultError EnhanceWithError(this IResultError resultError, IResultErrorNonComposite error) =>
        ResultErrorCompositeExtensions.ToResultError(error, resultError);
}

public static partial class ResultExtensions
{
    /// <summary>
    /// If async <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then add additional error context
    /// </summary>
    public static Task<Result> EnhanceWithErrorAsync(this Task<Result> resultTask, Func<string> errorMessage)
    {
        Result Execute(Result result) => result.EnhanceWithError(errorMessage);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// If async <see cref="Result"/> <see cref="Result.IsFailure"/> is true, then add additional error context
    /// </summary>
    public static Task<Result> EnhanceWithErrorAsync(this Task<Result> resultTask, IResultErrorNonComposite error)
    {
        Result Execute(Result result) => result.EnhanceWithError(error);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// If async <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then add additional error context
    /// </summary>
    public static Task<Result<T>> EnhanceWithErrorAsync<T>(this Task<Result<T>> resultTask, Func<string> errorMessage)
    {
        Result<T> Execute(Result<T> result) => result.EnhanceWithError(errorMessage);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// If async <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then add additional error context
    /// </summary>
    public static Task<Result<T>> EnhanceWithErrorAsync<T>(this Task<Result<T>> resultTask, IResultErrorNonComposite error)
    {
        Result<T> Execute(Result<T> result) => result.EnhanceWithError(error);

        return resultTask.PipeAsync(Execute);
    }
}