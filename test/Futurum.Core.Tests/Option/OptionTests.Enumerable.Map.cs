using System.Linq;

using FluentAssertions;

using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsMapTests
{
    [Fact]
    public void Basic()
    {
        var numbers = Enumerable.Range(0, 5);
        var options = numbers.Select(i => i.ToOption());

        var values = options.Map(x => x + 1);

        values.Count().Should().Be(5);
        values.Should().BeEquivalentTo(numbers.Select(x => x + 1));
    }
}