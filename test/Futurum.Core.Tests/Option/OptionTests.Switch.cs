using System;

using FluentAssertions;

using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionSwitchTests
{
    public class WithoutPredicate
    {
        [Fact]
        public void HasNoValue()
        {
            var trueValue = Guid.NewGuid();
            var falseValue = Guid.NewGuid();

            var option = Option<Guid>.None;

            var returnValue = option.Switch(value => trueValue,
                                            () => falseValue);

            returnValue.Should().Be(falseValue);
        }

        [Fact]
        public void HasValue()
        {
            var inputValue = Guid.NewGuid();

            var trueValue = Guid.NewGuid();
            var falseValue = Guid.NewGuid();

            var passedValue = Guid.Empty;

            var option = inputValue.ToOption();

            var returnValue = option.Switch(value =>
                                            {
                                                passedValue = value;

                                                return trueValue;
                                            },
                                            () => falseValue);

            returnValue.Should().Be(trueValue);
            passedValue.Should().Be(inputValue);
        }
    }

    public class WithPredicate
    {
        [Fact]
        public void DoesntMatch()
        {
            var trueValue = Guid.NewGuid();
            var falseValue = Guid.NewGuid();

            var passedValue = Guid.Empty;

            var option = trueValue.ToOption();

            var returnValue = option.Switch(value => !value.Equals(trueValue),
                                            _ => trueValue,
                                            value =>
                                            {
                                                passedValue = value;

                                                return falseValue;
                                            });

            returnValue.Should().Be(falseValue);
            passedValue.Should().Be(trueValue);
        }

        [Fact]
        public void HasNoValue()
        {
            var trueValue = Guid.NewGuid();
            var falseValue = Guid.NewGuid();

            var option = Option<Guid>.None;

            var returnValue = option.Switch(value => trueValue,
                                            () => falseValue);

            returnValue.Should().Be(falseValue);
        }

        [Fact]
        public void Matches()
        {
            var trueValue = Guid.NewGuid();
            var falseValue = Guid.NewGuid();

            var passedValue = Guid.Empty;

            var option = trueValue.ToOption();

            var returnValue = option.Switch(value => value.Equals(trueValue),
                                            value =>
                                            {
                                                passedValue = value;

                                                return trueValue;
                                            },
                                            _ => falseValue);

            returnValue.Should().Be(trueValue);
            passedValue.Should().Be(trueValue);
        }
    }
}