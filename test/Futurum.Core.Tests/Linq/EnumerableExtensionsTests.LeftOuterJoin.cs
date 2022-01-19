using System.Linq;

using FluentAssertions;

using Futurum.Core.Linq;
using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Linq;

public class EnumerableExtensionsTestsLeftOuterJoin
{
    [Fact]
    public void left_and_right_matches()
    {
        var left = Enumerable.Range(0, 10);
        var right = Enumerable.Range(0, 10);

        var result = left.LeftOuterJoin(right,
                                        x => x,
                                        x => x,
                                        (l, r) => (l, r));

        left.SequenceEqual(right).Should().BeTrue();
        result.Select(x => x.l).SequenceEqual(left).Should().BeTrue();
        result.Select(x => x.r).Choose().SequenceEqual(right).Should().BeTrue();
        result.Select(x => x.l).SequenceEqual(result.Select(x => x.r).Choose()).Should().BeTrue();
    }

    [Fact]
    public void left_more_than_right()
    {
        var left = Enumerable.Range(1, 20);
        var right = Enumerable.Range(1, 10);

        var result = left.LeftOuterJoin(right,
                                        x => x,
                                        x => x,
                                        (l, r) => (l, r));

        left.SequenceEqual(right).Should().BeFalse();
        result.Select(x => x.l).SequenceEqual(left).Should().BeTrue();
        result.Where(x => x.r != 0).Select(x => x.r).Choose().SequenceEqual(right).Should().BeTrue();
        result.Where(x => x.r == 0).Select(x => x.l).SequenceEqual(left.Except(right)).Should().BeTrue();
    }
}