namespace Futurum.Core.Result;

public static partial class ResultAsyncEnumerableExtensions
{
    /// <summary>
    /// Filters <see cref="Result"/> <see cref="IAsyncEnumerable{T}"/> using <paramref name="func"/>.
    /// <para>This implicitly understands <see cref="IAsyncEnumerable{T}"/></para>
    /// </summary>
    public static Result<IAsyncEnumerable<T>> Filter<T>(this Result<IAsyncEnumerable<T>> result, Func<T, bool> func)
    {
        IAsyncEnumerable<T> Execute(IAsyncEnumerable<T> xs) => xs.Where(func);

        return result.Map(Execute);
    }
}