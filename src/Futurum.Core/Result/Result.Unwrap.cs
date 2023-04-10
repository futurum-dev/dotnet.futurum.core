namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Comes out of the <see cref="Result"/> monad. 
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, the do nothing.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsFailure"/> is true, the throw an exception
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static void Unwrap(this Result result)
    {
        if (result.IsFailure)
        {
            throw new Exception(result.Error.Value.ToErrorString());
        }
    }

    /// <summary>
    /// Comes out of the <see cref="Result"/> monad. 
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, the do return the value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsFailure"/> is true, the throw an exception
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static T Unwrap<T>(this Result<T> result)
    {
        if (result.IsFailure)
        {
            throw new Exception(result.Error.Value.ToErrorString());
        }

        return result.Value.Value;
    }

    /// <summary>
    /// Comes out of the <see cref="Result"/> monad. 
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, the do nothing.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsFailure"/> is true, the throw an exception
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task UnwrapAsync(this Task<Result> resultTask)
    {
        var result = await resultTask;

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Value.ToErrorString());
        }
    }

    /// <summary>
    /// Comes out of the <see cref="Result"/> monad. 
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, the do return the value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the <see cref="Result"/> <see cref="Result.IsFailure"/> is true, the throw an exception
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<T> UnwrapAsync<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Value.ToErrorString());
        }

        return result.Value.Value;
    }
}