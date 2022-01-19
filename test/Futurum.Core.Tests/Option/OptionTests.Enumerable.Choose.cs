using System.Linq;

using FluentAssertions;

using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsChooseTests
{
    public class WithoutSelectorFunc
    {
        [Fact]
        public void AllHasNoValue()
        {
            var option1 = Option<int>.None;
            var option2 = Option<int>.None;
            var option3 = Option<int>.None;

            var values = new[] {option1, option2, option3}.Choose()
                                                          .ToList();

            values.Count.Should().Be(0);
        }

        [Fact]
        public void AllHasValue()
        {
            var option1 = Core.Option.Option.From(1);
            var option2 = Core.Option.Option.From(2);
            var option3 = Core.Option.Option.From(3);

            var values = new[] {option1, option2, option3}.Choose()
                                                          .ToList();

            values.Count.Should().Be(3);
            values[0].Should().Be(1);
            values[1].Should().Be(2);
            values[2].Should().Be(3);
        }

        [Fact]
        public void Mixed()
        {
            var option1 = Core.Option.Option.From(1);
            var option2 = Option<int>.None;
            var option3 = Option<int>.None;

            var values = new[] {option1, option2, option3}.Choose()
                                                          .ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be(1);
        }
    }

    public class WithSelectorFunc
    {
        [Fact]
        public void AllHasNoValue()
        {
            var values = new[] {1, 2, 3}.Choose(x => Option<int>.None)
                                        .ToList();

            values.Count.Should().Be(0);
        }

        [Fact]
        public void AllHasValue()
        {
            var values = new[] {1, 2, 3}.Choose(x => Core.Option.Option.From(x * 2))
                                        .ToList();

            values.Count.Should().Be(3);
            values[0].Should().Be(2);
            values[1].Should().Be(4);
            values[2].Should().Be(6);
        }

        [Fact]
        public void Mixed()
        {
            var values = new[] {1, 2, 3}.Choose(x => x == 1
                                                    ? Core.Option.Option.From(x * 2)
                                                    : Option<int>.None)
                                        .ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be(2);
        }
    }
}