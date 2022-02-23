using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultAsyncEnumerableExtensionsChooseTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class WithoutSelectorFunc
    {
        [Fact]
        public async Task AllFailure()
        {
            var result1 = Core.Result.Result.Fail<int>("Failure 1");
            var result2 = Core.Result.Result.Fail<int>("Failure 2");
            var result3 = Core.Result.Result.Fail<int>("Failure 3");

            var values = await ToAsyncEnumerable(new[] {result1, result2, result3}).Choose()
                                                                             .ToListAsync();

            values.Count.Should().Be(0);
        }

        [Fact]
        public async Task AllSuccess()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var result1 = Core.Result.Result.Ok(value1);
            var result2 = Core.Result.Result.Ok(value2);
            var result3 = Core.Result.Result.Ok(value3);

            var values = await ToAsyncEnumerable(new[] {result1, result2, result3}).Choose()
                                                                                   .ToListAsync();

            values.Count.Should().Be(3);
            values[0].Should().Be(value1);
            values[1].Should().Be(value2);
            values[2].Should().Be(value3);
        }

        [Fact]
        public async Task Mixed()
        {
            const int value1 = 1;

            var result1 = Core.Result.Result.Ok(value1);
            var result2 = Core.Result.Result.Fail<int>("Failure 1");
            var result3 = Core.Result.Result.Fail<int>("Failure 2");

            var values = await ToAsyncEnumerable(new[] {result1, result2, result3}).Choose()
                                                                                  .ToListAsync();

            values.Count.Should().Be(1);
            values[0].Should().Be(value1);
        }
    }

    public class WithSelectorFunc
    {
        private static int Transform(int x) =>
            x * 2;

        [Fact]
        public async Task AllFailure()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var values = await ToAsyncEnumerable(new[] {value1, value2, value3}).Choose(x => Core.Result.Result.Fail<int>(ErrorMessage))
                                                                               .ToListAsync();

            values.Count.Should().Be(0);
        }

        [Fact]
        public async Task AllSuccess()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var values = await ToAsyncEnumerable(new[] {value1, value2, value3}).Choose(x => Core.Result.Result.Ok(Transform(x)))
                                                                               .ToListAsync();

            values.Count.Should().Be(3);
            values[0].Should().Be(Transform(value1));
            values[1].Should().Be(Transform(value2));
            values[2].Should().Be(Transform(value3));
        }

        [Fact]
        public async Task Mixed()
        {
            const int value1 = 1;
            const int value2 = 2;
            const int value3 = 3;

            var values = await ToAsyncEnumerable(new[] {value1, value2, value3}).Choose(x => x == 1
                                                                                           ? Core.Result.Result.Ok(Transform(x))
                                                                                           : Core.Result.Result.Fail<int>(ErrorMessage))
                                                                               .ToListAsync();

            values.Count.Should().Be(1);
            values[0].Should().Be(Transform(value1));
        }
    }
    
    private static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(IEnumerable<T> values)
    {
        await Task.Yield();

        foreach (var value in values)
        {
            yield return value;
        }
    }
}