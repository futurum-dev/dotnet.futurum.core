using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionEnumerableExtensionsPickTests
{
    public class WithoutSelectorFunc
    {
        [Fact]
        public void AllHasNoValue()
        {
            var option1 = Option<int>.None;
            var option2 = Option<int>.None;
            var option3 = Option<int>.None;

            var option = new[] {option1, option2, option3}.Pick();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void AllHasValue()
        {
            var option1 = Core.Option.Option.From(1);
            var option2 = Core.Option.Option.From(2);
            var option3 = Core.Option.Option.From(3);

            var option = new[] {option1, option2, option3}.Pick();

            option.ShouldBeHasValueWithValue(1);
        }

        [Fact]
        public void Mixed()
        {
            var option1 = Core.Option.Option.From(1);
            var option2 = Option<int>.None;
            var option3 = Option<int>.None;

            var option = new[] {option1, option2, option3}.Pick();

            option.ShouldBeHasValueWithValue(1);
        }
    }

    public class WithSelectorFunc
    {
        [Fact]
        public void AllHasNoValue()
        {
            var option = new[] {1, 2, 3}.Pick(x => Option<int>.None);

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void AllHasValue()
        {
            var option = new[] {1, 2, 3}.Pick(x => Core.Option.Option.From(x * 2));

            option.ShouldBeHasValueWithValue(2);
        }

        [Fact]
        public void Mixed()
        {
            var option = new[] {1, 2, 3}.Pick(x => x == 1
                                                  ? Core.Option.Option.From(x * 2)
                                                  : Option<int>.None);

            option.ShouldBeHasValueWithValue(2);
        }
    }
}