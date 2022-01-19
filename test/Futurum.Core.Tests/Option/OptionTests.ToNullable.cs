using FluentAssertions;

using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionExtensionsToNullableTests
{
    public class WithoutSelctor
    {
        [Fact]
        public void HasNoValue()
        {
            int? value = null;

            var option = value.ToOption();

            var nullable = option.ToNullable();

            nullable.HasValue.Should().BeFalse();
        }

        [Fact]
        public void HasValue()
        {
            int? value = 1;

            var option = value.ToOption();

            var nullable = option.ToNullable();

            nullable.HasValue.Should().BeTrue();
            nullable.Value.Should().Be(value);
        }
    }

    public class WithSelctor
    {
        public class Container
        {
            public Container(int number)
            {
                Number = number;
            }

            public int Number { get; }
        }

        [Fact]
        public void HasNoValue()
        {
            Container value = null;

            var option = value.ToOption();

            var nullable = option.ToNullable(x => x.Number);

            nullable.HasValue.Should().BeFalse();
        }

        [Fact]
        public void HasValue()
        {
            var value = 1;

            var option = new Container(value).ToOption();

            var nullable = option.ToNullable(x => x.Number);

            nullable.HasValue.Should().BeTrue();
            nullable.Value.Should().Be(value);
        }
    }
}