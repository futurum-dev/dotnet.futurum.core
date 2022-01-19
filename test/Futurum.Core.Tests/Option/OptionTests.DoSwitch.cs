using System;

using FluentAssertions;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionDoSwitchTests
{
    [Fact]
    public void HasNoValue()
    {
        string inputValue = null;

        var trueCalled = false;
        var falseCalled = false;

        var option = inputValue.ToOption();

        var returnValue = option.DoSwitch(_ => trueCalled = true,
                                          () => falseCalled = true);

        returnValue.ShouldBeHasNoValue();
        falseCalled.Should().BeTrue();
        trueCalled.Should().BeFalse();
    }

    [Fact]
    public void HasValue()
    {
        var inputValue = Guid.NewGuid();

        var trueCalled = false;
        var falseCalled = false;

        var passedValue = Guid.Empty;

        var option = inputValue.ToOption();

        var returnValue = option.DoSwitch(value =>
                                          {
                                              passedValue = value;

                                              trueCalled = true;
                                          },
                                          () => falseCalled = true);

        returnValue.ShouldBeHasValueWithValue(inputValue);
        passedValue.Should().Be(inputValue);
        trueCalled.Should().BeTrue();
        falseCalled.Should().BeFalse();
    }
}