using System;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsMapSwitchTests
{
    [Fact]
    public void AllHasNoValue()
    {
        var trueValue = Guid.NewGuid();
        var falseValue = Guid.NewGuid();

        var options = Enumerable.Range(0, 10)
                                .Select(_ => Option<Guid>.None);

        var returnValues = options.MapSwitch(value => trueValue,
                                             () => falseValue);

        returnValues.Count().Should().Be(10);
        returnValues.Should().AllBeEquivalentTo(falseValue);
    }

    [Fact]
    public void AllHasValue()
    {
        var trueValue = Guid.NewGuid();
        var falseValue = Guid.NewGuid();

        var options = Enumerable.Range(0, 10)
                                .Select(i => i.ToOption());

        var returnValues = options.MapSwitch(value => trueValue,
                                             () => falseValue);

        returnValues.Count().Should().Be(10);
        returnValues.Should().AllBeEquivalentTo(trueValue);
    }

    [Fact]
    public void Maybe()
    {
        var trueValue = Guid.NewGuid();
        var falseValue = Guid.NewGuid();

        var options = Enumerable.Range(0, 2)
                                .Select(i => i % 2 == 0 ? i.ToOption() : Option<int>.None);

        var returnValues = options.MapSwitch(value => trueValue,
                                             () => falseValue)
                                  .ToList();

        returnValues.Count.Should().Be(2);
        returnValues[0].Should().Be(trueValue);
        returnValues[1].Should().Be(falseValue);
    }
}