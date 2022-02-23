using System.Collections.Generic;
using System.Linq;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultAsyncEnumerableExtensionsFilterTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    private static bool FilterFunction(int x)
        => true;

    [Fact]
    public void Failure()
    {
        var resultInput = Core.Result.Result.Fail<IAsyncEnumerable<int>>(ErrorMessage);

        var returnedResult = resultInput.Filter(FilterFunction);

        returnedResult.ShouldBeFailureWithError(ErrorMessage);
    }

    [Fact]
    public void Success()
    {
        var values = AsyncEnumerable.Range(0, 1);

        var resultInput = Core.Result.Result.Ok(values);

        var returnedResult = resultInput.Filter(FilterFunction);

        returnedResult.ShouldBeSuccess();
        returnedResult.ShouldBeSuccessWithValueEquivalentToAsync(values.Where(FilterFunction));
    }
}