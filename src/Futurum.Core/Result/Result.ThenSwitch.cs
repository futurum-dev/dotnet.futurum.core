using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result"/> <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields false, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TR> ThenSwitch<T, TR>(this Result<T> result, Func<T, bool> predicate, Func<T, Result<TR>> successFunc, Func<Result<TR>> failureFunc)
    {
        Result<TR> Execute(T value) => predicate(value) ? successFunc(value) : failureFunc();

        return result.Then(Execute);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result"/> <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields false, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TR> ThenSwitch<T, TR>(this Result<T> result, Func<T, Result> predicate, Func<T, Result<TR>> successFunc, Func<IResultError, Result<TR>> failureFunc)
    {
        Result<TR> Execute(T value) => predicate(value).Switch(() => successFunc(value), failureFunc);

        return result.Then(Execute);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result"/> <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields false, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TR> ThenSwitch<T, TR>(this Result<T> result, Func<T, bool> predicate, Func<T, Result<TR>> successFunc, Func<T, Result<TR>> failureFunc)
    {
        Result<TR> Execute(T value) => predicate(value) ? successFunc(value) : failureFunc(value);

        return result.Then(Execute);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result"/> <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields false, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenSwitchAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Result<TR>> successFunc, Func<Result<TR>> failureFunc)
    {
        Result<TR> Execute(Result<T> result) => result.ThenSwitch(predicate, successFunc, failureFunc);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Transforms <see cref="Result{T}"/> to <see cref="Result"/> <see cref="TR"/>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields true, then call and return <paramref name="successFunc"/></description>
    ///     </item>
    ///     <item>
    ///         <description>If <see cref="Result{T}"/> <see cref="Result{T}.IsSuccess"/> is true and <paramref name="predicate"/> yields false, then call and return <paramref name="failureFunc"/></description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenSwitchAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<T, Result<TR>> successFunc, Func<T, Result<TR>> failureFunc)
    {
        Result<TR> Execute(Result<T> result) => result.ThenSwitch(predicate, successFunc, failureFunc);

        return resultTask.PipeAsync(Execute);
    }
}