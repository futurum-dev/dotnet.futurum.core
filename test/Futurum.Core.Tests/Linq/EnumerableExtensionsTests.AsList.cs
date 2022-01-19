using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Linq;

using Xunit;

namespace Futurum.Core.Tests.Linq;

public class EnumerableExtensionsTestsAsList
{
    [Fact]
    public void IEnumerable()
    {
        var values = Enumerable.Range(0, 10);

        var result = values.AsList();

        (result.GetType() == typeof(List<int>)).Should().BeTrue();
    }

    [Fact]
    public void List()
    {
        var values = Enumerable.Range(0, 10)
                               .ToList();

        var result = values.AsList();

        (result.GetType() == typeof(List<int>)).Should().BeTrue();
    }
}