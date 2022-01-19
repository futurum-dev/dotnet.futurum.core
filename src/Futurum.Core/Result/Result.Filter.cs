namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Predicate for <see cref="Result"/> <see cref="Result.IsSuccess"/> true. 
    /// </summary>
    public static bool FilterSuccess(Result result) =>
        result.IsSuccess;

    /// <summary>
    /// Predicate for <see cref="Result"/> <see cref="Result.IsFailure"/> true. 
    /// </summary>
    public static bool FilterFailure(Result result) =>
        result.IsFailure;

    /// <summary>
    /// Predicate for <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> true. 
    /// </summary>
    public static bool FilterSuccess<T>(Result<T> result) =>
        result.IsSuccess;

    /// <summary>
    /// Predicate for <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> true. 
    /// </summary>
    public static bool FilterFailure<T>(Result<T> result) =>
        result.IsFailure;
}