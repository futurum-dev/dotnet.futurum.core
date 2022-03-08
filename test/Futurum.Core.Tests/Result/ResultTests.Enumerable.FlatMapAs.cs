using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Linq;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsFlatMapAsTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    private static int MapFunction(int x)
        => x * 2;
    
    public record Container(IEnumerable<int> Values);

    public class Sync
    {
        [Fact]
        public void Failure()
        {
            var resultInput = Core.Result.Result.Fail<IEnumerable<Container>>(ErrorMessage);

            var returnedResult = resultInput.FlatMapAs(x => x.Values, MapFunction);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public void Success()
        {
            var values = Enumerable.Range(0, 1)
                                   .Select(x => new Container(EnumerableExtensions.Return(x)));

            var resultInput = Core.Result.Result.Ok(values);

            var returnedResult = resultInput.FlatMapAs(x => x.Values, MapFunction);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(Enumerable.Range(0, 1).Select(MapFunction));
        }
    }

    public class Async
    {
        [Fact]
        public async Task Failure()
        {
            var resultInput = Core.Result.Result.FailAsync<IEnumerable<Container>>(ErrorMessage);

            var returnedResult = await resultInput.FlatMapAsAsync(x => x.Values, MapFunction);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public async Task Success()
        {
            var values = Enumerable.Range(0, 1)
                                   .Select(x => new Container(EnumerableExtensions.Return(x)));

            var resultInput = Core.Result.Result.OkAsync(values);

            var returnedResult = await resultInput.FlatMapAsAsync(x => x.Values, MapFunction);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(Enumerable.Range(0, 1).Select(MapFunction));
        }
    }
}