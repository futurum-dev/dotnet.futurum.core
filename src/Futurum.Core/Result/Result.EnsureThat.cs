using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    public static Result<T> EnsureThat<T>(this T value, Func<T, Result> predicate, Func<string> errorMessage)
    {
        Result<T> Success() => value.ToResultOk();

        Result<T> Failure(IResultError error) =>
            Result.Fail<T>(error.EnhanceWithError(errorMessage().ToResultError()));

        return predicate(value).Switch(Success, Failure);
    }

    public static Result<T> EnsureThat<T>(this T value, string propertyName, Func<T, string, Result> ensure) =>
        ensure(value, propertyName).Map(() => value);

    public static Result<T> EnsureThat<T, TValue>(this T value, Func<T, TValue> selector, string propertyName, Func<TValue, string, Result> ensure) =>
        ensure(selector(value), propertyName).Map(() => value);

    /// <summary>
    /// Checks an <see cref="Result{T}" />'s <see cref="Result{T}.Value" /> matches the <paramref name="predicate" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If true, then return a successful <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If false, then return a failure <see cref="Result{T}" /> using the specified
    ///         <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// If <paramref name="result" /> is a failure, then pass on the failure and do not do anything.
    /// </summary>
    public static Result<T> EnsureThat<T>(this Result<T> result, Func<T, bool> predicate, Func<string> errorMessage)
    {
        static Result<T> Success(T value) => Result.Ok(value);

        Result<T> Fail() => Result.Fail<T>(errorMessage());

        return result.ThenSwitch(predicate, Success, Fail);
    }

    /// <summary>
    /// Checks an <see cref="Result{T}" />'s <see cref="Result{T}.Value" /> matches the <paramref name="predicate" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If true, then return a successful <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If false, then return a failure <see cref="Result{T}" /> using the specified
    ///         <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// If <paramref name="result" /> is a failure, then pass on the failure and do not do anything.
    /// </summary>
    public static Result<T> EnsureThat<T>(this Result<T> result, Func<T, Result> predicate, Func<string> errorMessage)
    {
        static Result<T> Success(T value) => Result.Ok(value);

        Result<T> Failure(IResultError error) =>
            Result.Fail<T>(error.EnhanceWithError(errorMessage().ToResultError()));

        return result.ThenSwitch(predicate, Success, Failure);
    }

    public static Result<T> EnsureThat<T, TValue>(this Result<T> result, Func<T, TValue> selector, Func<TValue, Result> predicate, Func<string> errorMessage)
    {
        static Result<TValue> Success(TValue value) => Result.Ok(value);

        Result<TValue> Failure(IResultError error) =>
            Result.Fail<TValue>(error.EnhanceWithError(errorMessage().ToResultError()));

        return result.Map(selector)
                     .ThenSwitch(predicate, Success, Failure)
                     .Then(_ => result);
    }

    public static Result<T> EnsureThat<T>(this Result<T> result, string propertyName, Func<T, string, Result> ensure) =>
        result.Then(value => ensure(value, propertyName))
              .Then(_ => result);

    public static Result<T> EnsureThat<T, TValue>(this Result<T> result, Func<T, TValue> selector, string propertyName, Func<TValue, string, Result> ensure) =>
        result.Map(selector)
              .Then(value => ensure(value, propertyName))
              .Then(_ => result);

    /// <summary>
    /// Checks if <paramref name="resultTask" /> matches the <paramref name="predicate" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If true, then return a successful async <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If false, then return a failure async <see cref="Result{T}" /> using the specified
    ///         <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// If <paramref name="resultTask" /> is a failure, then pass on the failure and do not do anything.
    /// </summary>
    public static Task<Result<T>> EnsureThatAsync<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate, Func<string> errorMessage) =>
        resultTask.PipeAsync(EnsureThat, predicate, errorMessage);

    /// <summary>
    /// Checks if <paramref name="resultTask" /> matches the <paramref name="predicate" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If true, then return a successful async <see cref="Result{T}" />.</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If false, then return a failure async <see cref="Result{T}" /> using the specified
    ///         <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// <para></para>
    /// If <paramref name="resultTask" /> is a failure, then pass on the failure and do not do anything.
    /// </summary>
    public static Task<Result<T>> EnsureThatAsync<T>(this Task<Result<T>> resultTask, Func<T, Result> predicate, Func<string> errorMessage) =>
        resultTask.PipeAsync(EnsureThat, predicate, errorMessage);

    public static Task<Result<T>> EnsureThatAsync<T, TValue>(this Task<Result<T>> resultTask, Func<T, TValue> selector, Func<TValue, Result> predicate, Func<string> errorMessage) =>
        resultTask.PipeAsync(EnsureThat, selector, predicate, errorMessage);

    public static Task<Result<T>> EnsureThatAsync<T>(this Task<Result<T>> resultTask, string propertyName, Func<T, string, Result> ensure) =>
        resultTask.PipeAsync(EnsureThat, propertyName, ensure);

    public static Task<Result<T>> EnsureThatAsync<T, TValue>(this Task<Result<T>> resultTask, Func<T, TValue> selector, string propertyName, Func<TValue, string, Result> ensure) =>
        resultTask.PipeAsync(EnsureThat, selector, propertyName, ensure);
}