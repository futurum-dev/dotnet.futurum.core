using Futurum.Core.Option;

namespace Futurum.Core.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> left,
                                                                                   IEnumerable<TRight> right,
                                                                                   Func<TLeft, TKey> leftKey,
                                                                                   Func<TRight, TKey> rightKey,
                                                                                   Func<TLeft, Option<TRight>, TResult> result) =>
        left.GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
            .SelectMany(x => x.r.DefaultIfEmpty(),
                        (l, r) => new { l.l, r })
            .Select(x => result(x.l, x.r.ToOption()));
}