using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Functional;

using Xunit;

namespace Futurum.Core.Tests.Functional;

public class PipeExtensionsTests
{
    public class PipeAsync
    {
        [Fact]
        public async Task IEnumerable()
        {
            var values = Enumerable.Range(0, 10);

            var taskValues = values
                             .Select(Task.FromResult)
                             .ToList();

            var result = await taskValues.PipeAsync(x => string.Join(",", x));

            result.Should().Be(string.Join(",", values));
        }

        [Fact]
        public async Task NonResult()
        {
            var value = 10;

            var result = await Task.FromResult(value).PipeAsync(x => x.ToString());

            result.Should().Be(value.ToString());
        }

        [Fact]
        public async Task Result()
        {
            var value = 10;

            var result = await Task.FromResult(value).PipeAsync(x => Task.FromResult(x.ToString()));

            result.Should().Be(value.ToString());
        }
    }

    [Fact]
    public void Pipe()
    {
        var value = 10;

        var result = value.Pipe(x => x.ToString());

        result.Should().Be(value.ToString());
    }
}