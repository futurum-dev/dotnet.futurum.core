using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsFilterTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    private static bool FilterFunction(int x)
        => true;

    public class Sync
    {
        [Fact]
        public void Failure()
        {
            var resultInput = Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage);

            var returnedResult = resultInput.Filter(FilterFunction);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public void Success()
        {
            var values = Enumerable.Range(0, 1);

            var resultInput = Core.Result.Result.Ok(values);

            var returnedResult = resultInput.Filter(FilterFunction);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(values.Where(FilterFunction));
        }
    }

    public class Async
    {
        [Fact]
        public async Task Failure()
        {
            var resultInput = Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage);

            var returnedResult = await resultInput.FilterAsync(FilterFunction);

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public async Task Success()
        {
            var values = Enumerable.Range(0, 1);

            var resultInput = Core.Result.Result.OkAsync(values);

            var returnedResult = await resultInput.FilterAsync(FilterFunction);

            returnedResult.ShouldBeSuccess();
            returnedResult.Value.Value.Should().BeEquivalentTo(values.Where(FilterFunction));
        }
    }
}