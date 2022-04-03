using Futurum.Core.Result;

namespace Futurum.Core.Option;

public static partial class ResultOptionExtensions
{
    /// <summary>
    /// Transforms a <see cref="Result{T}"/> <see cref="Option{T}"/> to a <see cref="Result{T}" />
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResult<T>(this Result<Option<T>> resultOption, Func<string> errorMessage) =>
        resultOption.IsSuccess 
            ? resultOption.Value.Value.ToResult(() => errorMessage().ToResultError()) 
            : Result.Result.Fail<T>(resultOption.Error.Value);

    /// <summary>
    /// Transforms a <see cref="Result{T}"/> <see cref="Option{T}"/> to a <see cref="Result{T}" />
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResult<T>(this Result<Option<T>> resultOption, Func<IResultError> error) =>
        resultOption.IsSuccess 
            ? resultOption.Value.Value.ToResult(error) 
            : Result.Result.Fail<T>(resultOption.Error.Value);

    /// <summary>
    /// Transforms a <see cref="Result{T}"/> <see cref="Option{T}"/> to a <see cref="Result{T}" />
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Result<Option<T>>> resultOptionTask, Func<string> errorMessage)
    {
        var resultOption = await resultOptionTask;

        return resultOption.IsSuccess 
            ? resultOption.Value.Value.ToResult(() => errorMessage().ToResultError()) 
            : Result.Result.Fail<T>(resultOption.Error.Value);
    }

    /// <summary>
    /// Transforms a <see cref="Result{T}"/> <see cref="Option{T}"/> to a <see cref="Result{T}" />
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false,
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Result<Option<T>>> resultOptionTask, Func<IResultError> error)
    {
        var resultOption = await resultOptionTask;

        return resultOption.IsSuccess 
            ? resultOption.Value.Value.ToResult(error) 
            : Result.Result.Fail<T>(resultOption.Error.Value);
    }
}