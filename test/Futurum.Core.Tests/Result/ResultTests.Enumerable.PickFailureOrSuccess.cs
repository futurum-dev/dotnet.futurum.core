using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsPickFailureOrSuccessTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";
    private const string ErrorMessage3 = "ERROR_MESSAGE_3";

    [Fact]
    public void AllFailure()
    {
        var result1 = Core.Result.Result.Fail(ErrorMessage1);
        var result2 = Core.Result.Result.Fail(ErrorMessage2);
        var result3 = Core.Result.Result.Fail(ErrorMessage3);

        var result = new[] {result1, result2, result3}.PickFailureOrSuccess();

        result.ShouldBeFailureWithError(ErrorMessage1);
    }

    [Fact]
    public void AllSuccess()
    {
        var result1 = Core.Result.Result.Ok();
        var result2 = Core.Result.Result.Ok();
        var result3 = Core.Result.Result.Ok();

        var result = new[] {result1, result2, result3}.PickFailureOrSuccess();

        result.ShouldBeSuccess();
    }

    [Fact]
    public void Mixed()
    {
        var result1 = Core.Result.Result.Ok();
        var result2 = Core.Result.Result.Fail(ErrorMessage1);
        var result3 = Core.Result.Result.Fail(ErrorMessage2);

        var result = new[] {result1, result2, result3}.PickFailureOrSuccess();

        result.ShouldBeFailureWithError(ErrorMessage1);
    }
}