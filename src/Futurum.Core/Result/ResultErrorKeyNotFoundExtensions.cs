using Futurum.Core.Option;

namespace Futurum.Core.Result;

public static class ResultErrorKeyNotFoundExtensions
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
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true and <see cref="Result{T}.Error"/> set to <see cref="ResultErrorKeyNotFound"/>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResultErrorKeyNotFound<T>(this Result<Option<T>> resultOption, string key, string sourceDescription) =>
        resultOption.ToResult(() => ResultErrorKeyNotFound.Create(key, sourceDescription));

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
    ///         then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true and <see cref="Result{T}.Error"/> set to <see cref="ResultErrorKeyNotFound"/>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ToResultErrorKeyNotFoundAsync<T>(this Task<Result<Option<T>>> resultOptionTask, string key, string sourceDescription) =>
        resultOptionTask.ToResultAsync(() => ResultErrorKeyNotFound.Create(key, sourceDescription));
}