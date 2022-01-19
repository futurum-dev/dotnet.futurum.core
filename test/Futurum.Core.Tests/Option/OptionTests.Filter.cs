using FluentAssertions;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionFilterTests
{
    private const string ToStringValue = "TO_STRING";

    private class StubClass
    {
        public override string ToString() =>
            ToStringValue;
    }

    [Fact]
    public void FilterHasNoValue()
    {
        var option = Core.Option.Option.None<StubClass>();

        var hasNoValue = Core.Option.Option.FilterHasNoValue(option);

        hasNoValue.Should().BeTrue();
    }

    [Fact]
    public void FilterHasValue()
    {
        var instance = new StubClass();

        var option = Core.Option.Option.From(instance);

        var hasValue = Core.Option.Option.FilterHasValue(option);

        hasValue.Should().BeTrue();
    }
}