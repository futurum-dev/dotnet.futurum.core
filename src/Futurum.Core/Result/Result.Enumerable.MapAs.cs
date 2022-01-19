using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Transforms <see cref="Result"/> <see cref="IEnumerable{T}"/> to <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Result<IEnumerable<TR>> MapAs<T, TR>(this Result<IEnumerable<T>> result, Func<T, TR> func)
    {
        IEnumerable<TR> Execute(IEnumerable<T> xs) => xs.Select(func);

        return result.Map(Execute);
    }

    /// <summary>
    /// Transforms an async <see cref="Result"/> <see cref="IEnumerable{T}"/> to an async <see cref="Result"/> <see cref="IEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> MapAsAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultTask, Func<T, TR> func) =>
        resultTask.PipeAsync(MapAs, func);

    public static Task<Result<IEnumerable<TR2>>> MapAsAsync<T, TR1, TR2>(this Task<Result<T>> resultTask, Func<T, IEnumerable<TR1>> selectorFunc, Func<TR1, TR2> func)
    {
        Result<IEnumerable<TR2>> Execute(Result<T> result) => result.Map(selectorFunc).MapAs(func);

        return resultTask.PipeAsync(Execute);
    }
}