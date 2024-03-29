﻿using Futurum.Core.Result;

namespace Futurum.Core.Option;

public static partial class OptionExtensions
{
    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResult<T>(this Option<T> option, string errorMessage) =>
        option.HasValue ? Result.Result.Ok(option.Value) : errorMessage.ToResultError().ToFailResult<T>();
    
    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResult<T>(this Option<T> option, Func<string> errorMessage) =>
        option.HasValue ? Result.Result.Ok(option.Value) : errorMessage().ToResultError().ToFailResult<T>();

    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="error" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResult<T>(this Option<T> option, IResultError error) =>
        option.HasValue ? Result.Result.Ok(option.Value) : error.ToFailResult<T>();

    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="error" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ToResult<T>(this Option<T> option, Func<IResultError> error) =>
        option.HasValue ? Result.Result.Ok(option.Value) : error().ToFailResult<T>();

    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Option<T>> optionTask, string errorMessage)
    {
        var option = await optionTask;

        return option.HasValue ? Result.Result.Ok(option.Value) : errorMessage.ToFailResult<T>();
    }

    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="errorMessage" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Option<T>> optionTask, Func<string> errorMessage)
    {
        var option = await optionTask;

        return option.HasValue ? Result.Result.Ok(option.Value) : errorMessage().ToFailResult<T>();
    }

    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="error" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Option<T>> optionTask, IResultError error)
    {
        var option = await optionTask;

        return option.HasValue ? Result.Result.Ok(option.Value) : error.ToFailResult<T>();
    }

    /// <summary>
    /// Transforms an <see cref="Option{T}" /> to a <see cref="Result{T}" />.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasValue" /> is true, then return a successful <see cref="Result{T}" />.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <see cref="Option{T}" /> <see cref="Option{T}.HasNoValue" /> is true, then return a failure <see cref="Result{T}" /> using the specified <paramref name="error" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Option<T>> optionTask, Func<IResultError> error)
    {
        var option = await optionTask;

        return option.HasValue ? Result.Result.Ok(option.Value) : error().ToFailResult<T>();
    }
}