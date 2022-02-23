namespace Futurum.Core.Result;

public static partial class ResultAsyncEnumerableExtensions
{
    /// <summary>
    /// Transforms <see cref="Result"/> <see cref="IAsyncEnumerable{T}"/> to <see cref="Result"/> <see cref="IAsyncEnumerable{TR}"/>
    /// <para>This implicitly understands <see cref="IAsyncEnumerable{T}"/></para>
    /// </summary>
    public static Result<IAsyncEnumerable<TR>> MapAs<T, TR>(this Result<IAsyncEnumerable<T>> result, Func<T, TR> func)
    {
        IAsyncEnumerable<TR> Execute(IAsyncEnumerable<T> xs) => xs.Select(func);

        return result.Map(Execute);
    }
}