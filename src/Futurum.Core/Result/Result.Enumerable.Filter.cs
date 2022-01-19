using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Filters <see cref="Result"/> <see cref="IEnumerable{T}"/> using <paramref name="func"/>.
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Result<IEnumerable<T>> Filter<T>(this Result<IEnumerable<T>> result, Func<T, bool> func)
    {
        IEnumerable<T> Execute(IEnumerable<T> xs) => xs.Where(func);

        return result.Map(Execute);
    }

    /// <summary>
    /// Filters <see cref="Result"/> <see cref="IEnumerable{T}"/> using <paramref name="func"/>.
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Task<Result<IEnumerable<T>>> FilterAsync<T>(this Task<Result<IEnumerable<T>>> resultTask, Func<T, bool> func) =>
        resultTask.PipeAsync(Filter, func);
}