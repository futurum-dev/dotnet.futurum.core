using FluentAssertions;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultFilterTests
{
    private const string ErrorMessage = "ERROR_MESSAGE1";

    public class NonGeneric
    {
        [Fact]
        public void FilterFailure()
        {
            var result = Core.Result.Result.Fail(ErrorMessage);

            var isFailure = Core.Result.Result.FilterFailure(result);

            isFailure.Should().BeTrue();
        }

        [Fact]
        public void FilterSuccess()
        {
            var result = Core.Result.Result.Ok();

            var isSuccess = Core.Result.Result.FilterSuccess(result);

            isSuccess.Should().BeTrue();
        }
    }

    public class Generic
    {
        [Fact]
        public void FilterFailure()
        {
            var result = Core.Result.Result.Fail<int>(ErrorMessage);

            var isFailure = Core.Result.Result.FilterFailure(result);

            isFailure.Should().BeTrue();
        }

        [Fact]
        public void FilterSuccess()
        {
            var result = Core.Result.Result.Ok(1);

            var isSuccess = Core.Result.Result.FilterSuccess(result);

            isSuccess.Should().BeTrue();
        }
    }
}