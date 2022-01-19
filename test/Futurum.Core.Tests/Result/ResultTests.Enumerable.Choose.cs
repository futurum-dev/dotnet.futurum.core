using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsChooseTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class WithoutSelectorFunc
    {
        [Fact]
        public void AllFailure()
        {
            var result1 = Core.Result.Result.Fail<int>("Failure 1");
            var result2 = Core.Result.Result.Fail<int>("Failure 2");
            var result3 = Core.Result.Result.Fail<int>("Failure 3");

            var values = new[] {result1, result2, result3}.Choose()
                                                          .ToList();

            values.Count.Should().Be(0);
        }

        [Fact]
        public void AllSuccess()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var result1 = Core.Result.Result.Ok(value1);
            var result2 = Core.Result.Result.Ok(value2);
            var result3 = Core.Result.Result.Ok(value3);

            var values = new[] {result1, result2, result3}.Choose()
                                                          .ToList();

            values.Count.Should().Be(3);
            values[0].Should().Be(value1);
            values[1].Should().Be(value2);
            values[2].Should().Be(value3);
        }

        [Fact]
        public void Mixed()
        {
            const int value1 = 1;

            var result1 = Core.Result.Result.Ok(value1);
            var result2 = Core.Result.Result.Fail<int>("Failure 1");
            var result3 = Core.Result.Result.Fail<int>("Failure 2");

            var values = new[] {result1, result2, result3}.Choose()
                                                          .ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be(value1);
        }
    }

    public class WithSelectorFunc
    {
        private static int Transform(int x) =>
            x * 2;

        [Fact]
        public void AllFailure()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var values = new[] {value1, value2, value3}.Choose(x => Core.Result.Result.Fail<int>(ErrorMessage))
                                                       .ToList();

            values.Count.Should().Be(0);
        }

        [Fact]
        public void AllSuccess()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var values = new[] {value1, value2, value3}.Choose(x => Core.Result.Result.Ok(Transform(x)))
                                                       .ToList();

            values.Count.Should().Be(3);
            values[0].Should().Be(Transform(value1));
            values[1].Should().Be(Transform(value2));
            values[2].Should().Be(Transform(value3));
        }

        [Fact]
        public void Mixed()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var values = new[] {value1, value2, value3}.Choose(x => x == 1
                                                                   ? Core.Result.Result.Ok(Transform(x))
                                                                   : Core.Result.Result.Fail<int>(ErrorMessage))
                                                       .ToList();

            values.Count.Should().Be(1);
            values[0].Should().Be(Transform(value1));
        }
    }
}