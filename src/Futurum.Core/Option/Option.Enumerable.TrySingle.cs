using Futurum.Core.Result;

namespace Futurum.Core.Option;

public static partial class OptionEnumerableExtensions
{
    /// <summary>
    /// Returns the only element of a sequence.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the sequence is empty, then an <see cref="Option{TSource}"/>.None is returned, wrapped in a <see cref="Result"/>.IsSuccess.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the sequence has 1 value, then the value is returned as a <see cref="Option{T}"/>, wrapped in a <see cref="Result"/>.IsSuccess.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the sequence has more than 1 value, then a <see cref="Result"/>.IsFailure is returned.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<Option<TSource>> TrySingle<TSource>(this IEnumerable<TSource> source)
    {
        Option<TSource> Execute() =>
            source.Select(Option<TSource>.From)
                  .SingleOrDefault();

        return Result.Result.Try(Execute, () => $"Failed {nameof(Enumerable.SingleOrDefault)} for type : '{typeof(TSource).FullName}'");
    }

    /// <summary>
    /// Returns the only element of a sequence.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If the sequence is empty, then an <see cref="Option{TSource}"/>.None is returned, wrapped in a <see cref="Result"/>.IsSuccess.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the sequence has 1 value, then the value is returned as a <see cref="Option{T}"/>, wrapped in a <see cref="Result"/>.IsSuccess.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If the sequence has more than 1 value, then a <see cref="Result"/>.IsFailure is returned.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<Option<TSource>> TrySingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) =>
        source.Where(predicate)
              .TrySingle();
}