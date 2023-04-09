namespace Futurum.Core.Result;

public static partial class ResultExtensions
{
    /// <summary>
    /// Ignores the failure on <see cref="Result"/> and always returns <see cref="Result.Ok"/>.
    /// </summary>
    public static Result IgnoreFailure(this Result result)
    {
        return Result.Ok();
    }
    
    /// <summary>
    /// Ignores the failure on <see cref="Result{T}"/> and always returns <see cref="Result.Ok"/>.
    /// </summary>
    public static async Task<Result> IgnoreFailureAsync(this Task<Result> result)
    {
        try
        {
            await result;
        }
        catch (Exception)
        {
        }
        
        return Result.Ok();
    }
}