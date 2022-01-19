using Futurum.Core.Functional;
using Futurum.Core.Result;

namespace Futurum.Core.Option;

public static partial class ResultOptionExtensions
{
    /// <summary>
    /// Transforms <see cref="Result{T}"/> <see cref="Option{T}"/> to <see cref="Result{T}"/> <typeparamref name="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true,
    ///         then call and return <paramref name="hasValueFunc"/> as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false,
    ///         then call and return <paramref name="hasNoValueFunc"/> as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TR> MapSwitch<T, TR>(this Result<Option<T>> resultOption, Func<T, TR> hasValueFunc, Func<TR> hasNoValueFunc)
    {
        TR Execute(Option<T> option) => option.Switch(hasValueFunc, hasNoValueFunc);

        return resultOption.Map(Execute);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> <see cref="Option{T}"/> to <see cref="Result{T}"/> <typeparamref name="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is true,
    ///         then call and return <paramref name="hasValueFunc"/> as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <see cref="Option{T}"/> <see cref="Option{T}.HasValue"/> is false,
    ///         then call and return <paramref name="hasNoValueFunc"/> as a <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Result{T}"/> <see cref="Result{T}.IsFailure"/> is true, then return a <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> MapMatchAsync<T, TR>(this Task<Result<Option<T>>> resultOptionTask, Func<T, TR> hasValueFunc, Func<TR> hasNoValueFunc)
    {
        Result<TR> Execute(Result<Option<T>> resultOption) => resultOption.MapSwitch(hasValueFunc, hasNoValueFunc);

        return resultOptionTask.PipeAsync(Execute);
    }
}