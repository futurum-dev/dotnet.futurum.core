namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Transforms <see cref="Result"/> <see cref="IEnumerable{T}"/> to <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Result<IEnumerable<TR>> FlatMapAs<T, T1, TR>(this Result<IEnumerable<T>> result, Func<T, IEnumerable<T1>> selectorFunc, Func<T1, TR> func)
    {
        IEnumerable<TR> Execute(IEnumerable<T> xs) => xs.SelectMany(selectorFunc).Select(func);

        return result.Map(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result"/> <see cref="IEnumerable{T}"/> to an async <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static async Task<Result<IEnumerable<TR>>> FlatMapAsAsync<T, T1, TR>(this Task<Result<IEnumerable<T>>> resultTask, Func<T, IEnumerable<T1>> selectorFunc, Func<T1, TR> func)
    {
        var result = await resultTask;

        return result.FlatMapAs(selectorFunc, func);
    }
}