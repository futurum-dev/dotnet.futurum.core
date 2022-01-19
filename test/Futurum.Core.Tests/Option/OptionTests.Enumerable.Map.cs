using System.Linq;

using FluentAssertions;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsMapTests
{
    [Fact]
    public void Basic()
    {
        var options = Enumerable.Range(0, 5)
                                .Select(i => i % 2 == 0 ? i.ToOption() : Core.Option.Option.None<int>());

        var values = options.Map(x => x.HasValue ? (x.Value * 2).ToOption() : Core.Option.Option.None<int>())
                            .ToList();

        values.Count.Should().Be(5);
        values[0].Should().Be(0);
        values[1].ShouldBeHasNoValue();
        values[2].Should().Be(4);
        values[3].ShouldBeHasNoValue();
        values[4].Should().Be(8);
    }
}