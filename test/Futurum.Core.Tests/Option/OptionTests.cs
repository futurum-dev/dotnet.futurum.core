using System;

using FluentAssertions;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionTests
{
    private const string ToStringValue = "TO_STRING";

    public class Behaviour
    {
        public class when_Some
        {
            [Fact]
            public void then_HasNoValue_is_false()
            {
                var value = Guid.NewGuid().ToString();

                var option = Core.Option.Option.From(value);

                option.HasNoValue.Should().BeFalse();
            }

            [Fact]
            public void then_HasValue_is_true()
            {
                var value = Guid.NewGuid().ToString();

                var option = Core.Option.Option.From(value);

                option.HasValue.Should().BeTrue();
            }

            [Fact]
            public void then_Value_returns_the_passed_in_payload()
            {
                var value = Guid.NewGuid().ToString();

                var option = Core.Option.Option.From(value);

                option.Value.Should().Be(value);
            }
        }

        public class when_None
        {
            [Fact]
            public void then_HasNoValue_is_true()
            {
                var option = Core.Option.Option.None<string>();

                option.HasNoValue.Should().BeTrue();
            }

            [Fact]
            public void then_HasValue_is_false()
            {
                var option = Core.Option.Option.None<string>();

                option.HasValue.Should().BeFalse();
            }

            [Fact]
            public void Throws_Exception_when_accessing_Value()
            {
                Futurum.Core.Option.Option<StubClass> option = null;

                Action action = () =>
                {
                    var _ = option.Value;
                };

                action.Should().Throw<InvalidOperationException>();
            }
        }
    }

    public class Equality
    {
        public class OptionToOption
        {
            [Fact]
            public void both_with_HasNoValue_are_equal()
            {
                Futurum.Core.Option.Option<StubClass> option1 = null;
                Futurum.Core.Option.Option<StubClass> option2 = null;

                option1.Equals(option2).Should().BeTrue();
                ((object) option1).Equals(option2).Should().BeTrue();
                (option1 == option2).Should().BeTrue();
                (option1 != option2).Should().BeFalse();
                (option1.GetHashCode() == option2.GetHashCode()).Should().BeTrue();
            }

            [Fact]
            public void different_content_are_not_equal()
            {
                Futurum.Core.Option.Option<StubClass> option1 = new StubClass();
                Futurum.Core.Option.Option<StubClass> option2 = new StubClass();

                option1.Equals(option2).Should().BeFalse();
                ((object) option1).Equals(option2).Should().BeFalse();
                (option1 == option2).Should().BeFalse();
                (option1 != option2).Should().BeTrue();
                (option1.GetHashCode() == option2.GetHashCode()).Should().BeFalse();
            }

            [Fact]
            public void one_with_HasNoValue_one_with_HasValue_are_not_equal()
            {
                Futurum.Core.Option.Option<StubClass> option1 = null;
                Futurum.Core.Option.Option<StubClass> option2 = new StubClass();

                option1.Equals(option2).Should().BeFalse();
                ((object) option1).Equals(option2).Should().BeFalse();
                (option1 == option2).Should().BeFalse();
                (option1 != option2).Should().BeTrue();
                (option1.GetHashCode() == option2.GetHashCode()).Should().BeFalse();
            }

            [Fact]
            public void one_with_HasValue_one_with_HasNoValue_are_not_equal()
            {
                Futurum.Core.Option.Option<StubClass> option1 = new StubClass();
                Futurum.Core.Option.Option<StubClass> option2 = null;

                option1.Equals(option2).Should().BeFalse();
                ((object) option1).Equals(option2).Should().BeFalse();
                (option1 == option2).Should().BeFalse();
                (option1 != option2).Should().BeTrue();
                (option1.GetHashCode() == option2.GetHashCode()).Should().BeFalse();
            }

            [Fact]
            public void same_content_are_equal()
            {
                var instance = new StubClass();

                Futurum.Core.Option.Option<StubClass> option1 = instance;
                Futurum.Core.Option.Option<StubClass> option2 = instance;

                option1.Equals(option2).Should().BeTrue();
                ((object) option1).Equals(option2).Should().BeTrue();
                (option1 == option2).Should().BeTrue();
                (option1 != option2).Should().BeFalse();
                (option1.GetHashCode() == option2.GetHashCode()).Should().BeTrue();
            }
        }

        public class OptionToUnderlyingType
        {
            [Fact]
            public void different_content_are_not_equal()
            {
                Futurum.Core.Option.Option<StubClass> option = new StubClass();
                var instance = new StubClass();

                option.Equals(instance).Should().BeFalse();
                ((object) option).Equals(instance).Should().BeFalse();
                (option == instance).Should().BeFalse();
                (option != instance).Should().BeTrue();
                (option.GetHashCode() == instance.GetHashCode()).Should().BeFalse();
            }

            [Fact]
            public void option_with_HasNoValue_underlyingtpe_with_value_are_not_equal()
            {
                Futurum.Core.Option.Option<StubClass> option = null;
                var instance = new StubClass();

                option.Equals(instance).Should().BeFalse();
                ((object) option).Equals(instance).Should().BeFalse();
                (option == instance).Should().BeFalse();
                (option != instance).Should().BeTrue();
                (option.GetHashCode() == instance.GetHashCode()).Should().BeFalse();
            }

            [Fact]
            public void same_content_are_equal()
            {
                var instance = new StubClass();
                Futurum.Core.Option.Option<StubClass> option = instance;

                option.Equals(instance).Should().BeTrue();
                ((object) option).Equals(instance).Should().BeTrue();
                (option == instance).Should().BeTrue();
                (option != instance).Should().BeFalse();
                (option.GetHashCode() == instance.GetHashCode()).Should().BeTrue();
            }
        }

        [Fact]
        public void NotAnOption()
        {
            Futurum.Core.Option.Option<StubClass> option = new StubClass();
            var other = 1;

            option.Equals(other).Should().BeFalse();
            ((object) option).Equals(other).Should().BeFalse();
            (option.GetHashCode() == other.GetHashCode()).Should().BeFalse();
        }
    }

    public class ToString
    {
        public class DefaultImplementationOnObject
        {
            [Fact]
            public void HasNoValue()
            {
                Futurum.Core.Option.Option<StubClass> option = null;

                option.ToString().Should().Be($"No value for {typeof(StubClass).FullName}");
            }

            [Fact]
            public void HasValue()
            {
                Futurum.Core.Option.Option<StubClass> option = new StubClass();

                option.ToString().Should().Be(ToStringValue);
            }
        }

        public class WithDefaultValue
        {
            [Fact]
            public void HasNoValue()
            {
                var defaultString = Guid.NewGuid().ToString();

                Futurum.Core.Option.Option<StubClass> option = null;

                option.ToString(defaultString).Should().Be(defaultString);
            }

            [Fact]
            public void HasValue()
            {
                var defaultString = Guid.NewGuid().ToString();

                Futurum.Core.Option.Option<StubClass> option = new StubClass();

                option.ToString(defaultString).Should().Be(ToStringValue);
            }
        }
    }

    public class ImplicitConversion
    {
        [Fact]
        public void HasNoValue()
        {
            StubClass instance = null;

            Futurum.Core.Option.Option<StubClass> option = instance;

            option.HasNoValue.Should().BeTrue();
            option.HasValue.Should().BeFalse();
        }

        [Fact]
        public void HasValue()
        {
            var instance = new StubClass();

            Futurum.Core.Option.Option<StubClass> option = instance;

            option.HasValue.Should().BeTrue();
            option.HasNoValue.Should().BeFalse();
            option.Value.Should().Be(instance);
        }
    }

    private class StubClass
    {
        public override string ToString() =>
            ToStringValue;
    }

    [Fact]
    public void DefaultInitializer()
    {
        var option = default(Futurum.Core.Option.Option<int>);

        option.HasNoValue.Should().BeTrue();
        option.HasValue.Should().BeFalse();
    }
}