namespace Futurum.Core.Result;

public readonly partial struct Result
{
    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result Try(Action func, Func<string> errorMessage)
    {
        try
        {
            func();

            return Ok();
        }
        catch (Exception exception)
        {
            return Fail(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result{T}.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result{T}.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> Try<T>(Func<T> func, Func<string> errorMessage)
    {
        try
        {
            var value = func();

            return Ok(value);
        }
        catch (Exception exception)
        {
            return Fail<T>(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result Try(Func<Result> func, Func<string> errorMessage)
    {
        try
        {
            var result = func();

            return result.EnhanceWithError(errorMessage);
        }
        catch (Exception exception)
        {
            return Fail(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result{T}.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result{T}.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> Try<T>(Func<Result<T>> func, Func<string> errorMessage)
    {
        try
        {
            var result = func();

            return result;
        }
        catch (Exception exception)
        {
            return Fail<T>(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> TryAsync(Func<Task> func, Func<string> errorMessage)
    {
        try
        {
            await func();

            return Ok();
        }
        catch (Exception exception)
        {
            return Fail(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result{T}.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result{T}.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> TryAsync<T>(Func<Task<T>> func, Func<string> errorMessage)
    {
        try
        {
            var value = await func();

            return Ok(value);
        }
        catch (Exception exception)
        {
            return Fail<T>(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result{T}.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result{T}.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> TryAsync<T>(Func<ValueTask<T>> func, Func<string> errorMessage)
    {
        try
        {
            var value = await func();

            return Ok(value);
        }
        catch (Exception exception)
        {
            return Fail<T>(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result> TryAsync(Func<Task<Result>> func, Func<string> errorMessage)
    {
        try
        {
            var result = await func();

            return result.EnhanceWithError(errorMessage);
        }
        catch (Exception exception)
        {
            return Fail(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>.
    /// <list type="bullet">
    ///     <item>
    ///         <description>If it is successful, return <see cref="Result{T}.Ok"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>If it fails, return <see cref="Result{T}.Fail"/> with the <paramref name="errorMessage"/>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> TryAsync<T>(Func<Task<Result<T>>> func, Func<string> errorMessage)
    {
        try
        {
            var result = await func();

            return result.EnhanceWithError(errorMessage);
        }
        catch (Exception exception)
        {
            return Fail<T>(exception.ToResultError(errorMessage()));
        }
    }
}