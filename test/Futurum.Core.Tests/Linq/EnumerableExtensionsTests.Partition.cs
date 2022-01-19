using System.Linq;

using FluentAssertions;

using Futurum.Core.Linq;

using Xunit;

namespace Futurum.Core.Tests.Linq;

public class EnumerableExtensionsTestsPartition
{
    [Fact]
    public void Empty()
    {
        var value = Enumerable.Empty<int>()
                              .ToList();

        var (matches, nonMatches) = value.Partition(x => x < 5);

        matches.Should().BeEquivalentTo(Enumerable.Empty<int>());
        nonMatches.Should().BeEquivalentTo(Enumerable.Empty<int>());
    }

    [Fact]
    public void MatchEmpty()
    {
        var value = Enumerable.Range(5, 5)
                              .ToList();

        var (matches, nonMatches) = value.Partition(x => x < 5);

        matches.Should().BeEquivalentTo(Enumerable.Empty<int>());
        nonMatches.Should().BeEquivalentTo(Enumerable.Range(5, 5));
    }

    [Fact]
    public void NonMatchEmpty()
    {
        var value = Enumerable.Range(0, 5)
                              .ToList();

        var (matches, nonMatches) = value.Partition(x => x < 5);

        matches.Should().BeEquivalentTo(Enumerable.Range(0, 5));
        nonMatches.Should().BeEquivalentTo(Enumerable.Empty<int>());
    }

    [Fact]
    public void Scenario1()
    {
        var value = Enumerable.Range(0, 10)
                              .ToList();

        var (matches, nonMatches) = value.Partition(x => x < 5);

        matches.Should().BeEquivalentTo(Enumerable.Range(0, 5));
        nonMatches.Should().BeEquivalentTo(Enumerable.Range(5, 5));
    }
}