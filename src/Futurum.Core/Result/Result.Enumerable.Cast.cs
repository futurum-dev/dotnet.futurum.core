using Futurum.Core.Functional;

namespace Futurum.Core.Result;

public static partial class ResultEnumerableExtensions
{
    /// <summary>
    /// Casts <see cref="Result"/> <see cref="IEnumerable{T}"/> to <see cref="Result"/> <see cref="IEnumerable{TR}"/> of the the specified type.
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Result<IEnumerable<TR>> Cast<T, TR>(this Result<IEnumerable<T>> result)
        where TR : T
    {
        IEnumerable<TR> Execute(IEnumerable<T> xs) => xs.Cast<TR>();

        return result.Map(Execute);
    }

    /// <summary>
    /// Casts <see cref="Result"/> <see cref="IEnumerable{T}"/> to <see cref="Result"/> <see cref="IEnumerable{TR}"/> of the the specified type.
    /// <para>This implicitly understands <see cref="IEnumerable{T}"/></para>
    /// </summary>
    public static Task<Result<IEnumerable<TR>>> CastAsync<T, TR>(this Task<Result<IEnumerable<T>>> resultTask)
        where TR : T =>
        resultTask.PipeAsync(Cast<T, TR>);
}