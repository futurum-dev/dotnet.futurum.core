namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Transforms <see cref="Result"/> <see cref="IEnumerable{T}"/> to <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Result<IEnumerable<TR>> ThenAs<T, TR>(this Result<IEnumerable<T>> result, Func<T, Result<TR>> func)
    {
        Result<IEnumerable<TR>> Execute(IEnumerable<T> xs) => xs.Select(func).Combine();

        return result.Then(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result"/> <see cref="IEnumerable{T}"/> to an async <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static async Task<Result<IEnumerable<TR>>> ThenAsAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultTask, Func<T, Result<TR>> func)
    {
        var result = await resultTask;
        
        return result.ThenAs(func);
    }

    /// <summary>
    /// Transforms an async <see cref="Result"/> <see cref="IEnumerable{T}"/> to an async <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static async Task<Result<IEnumerable<TR2>>> ThenAsAsync<T, TR1, TR2>(this Task<Result<T>> resultTask, Func<T, IEnumerable<TR1>> selectorFunc, Func<TR1, Result<TR2>> func)
    {
        var result = await resultTask;
        
        return result.Map(selectorFunc).ThenAs(func);
    }
}