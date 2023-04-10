using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultIgnoreFailureTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        [Fact]
        public void FailureInput()
        {
            var resultInput = Core.Result.Result.Fail(ErrorMessage);

            var resultOutput = resultInput.IgnoreFailure();

            resultOutput.ShouldBeSuccess();
        }

        [Fact]
        public void SuccessInput()
        {
            var resultInput = Core.Result.Result.Ok();

            var resultOutput = resultInput.IgnoreFailure();

            resultOutput.ShouldBeSuccess();
        }
    }

    public class Async
    {
        [Fact]
        public async Task FailureInput()
        {
            var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

            var resultOutput = await resultInput.IgnoreFailureAsync();

            resultOutput.ShouldBeSuccess();
        }

        [Fact]
        public async Task Exception()
        {
            var resultInput = ThrowException();

            var resultOutput = await resultInput.IgnoreFailureAsync();

            resultOutput.ShouldBeSuccess();

            Task<Core.Result.Result> ThrowException()
            {
                return Task.FromException<Core.Result.Result>(new Exception(ErrorMessage));
            }
        }

        [Fact]
        public async Task SuccessInput()
        {
            var resultInput = Core.Result.Result.OkAsync();

            var resultOutput = await resultInput.IgnoreFailureAsync();

            resultOutput.ShouldBeSuccess();
        }
    }
}