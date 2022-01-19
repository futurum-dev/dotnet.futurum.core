using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Linq;

using Xunit;

namespace Futurum.Core.Tests.Linq;

public class EnumerableExtensionsTestsOrEmptyIfNull
{
    [Fact]
    public void when_not_Null()
    {
        var value = Enumerable.Range(5, 5)
                              .ToList()
                              .AsEnumerable();

        var result = value.OrEmptyIfNull();

        result.Should().NotBeEmpty();
        result.Should().BeEquivalentTo(value);
    }

    [Fact]
    public void when_Null()
    {
        IEnumerable<int> value = null;

        var result = value.OrEmptyIfNull();

        result.Should().BeEmpty();
    }
}