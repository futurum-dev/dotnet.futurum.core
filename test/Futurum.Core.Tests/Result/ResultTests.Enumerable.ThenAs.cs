using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsThenAsTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    private static int MapFunction(int x)
        => x * 2;

    private static Result<int> MapFunctionReturnResult(int x)
        => MapFunction(x).ToResultOk();

    public class Sync
    {
        [Fact]
        public void Failure()
        {
            var resultInput = Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage);

            var returnedResult = resultInput.ThenAs(MapFunctionReturnResult);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public void Success()
        {
            var values = Enumerable.Range(0, 1);

            var resultInput = Core.Result.Result.Ok(values);

            var returnedResult = resultInput.ThenAs(MapFunctionReturnResult);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(values.Select(MapFunction));
        }
    }

    public class Async
    {
        [Fact]
        public async Task Failure()
        {
            var resultInput = Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage);

            var returnedResult = await resultInput.ThenAsAsync(MapFunctionReturnResult);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public async Task Success()
        {
            var values = Enumerable.Range(0, 1);

            var resultInput = Core.Result.Result.OkAsync(values);

            var returnedResult = await resultInput.ThenAsAsync(MapFunctionReturnResult);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(values.Select(MapFunction));
        }
    }

    public class AsyncWithSelector
    {
        public class Container
        {
            public Container(IEnumerable<int> values)
            {
                Values = values;
            }

            public IEnumerable<int> Values { get; }
        }

        [Fact]
        public async Task Failure()
        {
            var resultInput = Core.Result.Result.FailAsync<Container>(ErrorMessage);

            var returnedResult = await resultInput.ThenAsAsync(x => x.Values, MapFunctionReturnResult);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public async Task Success()
        {
            var values = Enumerable.Range(0, 1);

            var resultInput = Core.Result.Result.OkAsync(new Container(values));

            var returnedResult = await resultInput.ThenAsAsync(x => x.Values, MapFunctionReturnResult);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(values.Select(MapFunction));
        }
    }
}