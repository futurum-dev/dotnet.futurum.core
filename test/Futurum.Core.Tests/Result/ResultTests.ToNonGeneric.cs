using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultExtensionsToNonGenericTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        [Fact]
        public void Failure()
        {
            var resultInput = Core.Result.Result.Fail<string>(ErrorMessage);

            var returnedResult = resultInput.ToNonGeneric();

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public void Success()
        {
            var resultInput = Core.Result.Result.Ok(Guid.NewGuid().ToString());

            var returnedResult = resultInput.ToNonGeneric();

            returnedResult.ShouldBeSuccess();
        }
    }

    public class Async
    {
        [Fact]
        public async Task Failure()
        {
            var resultInput = Core.Result.Result.FailAsync<string>(ErrorMessage);

            var returnedResult = await resultInput.ToNonGenericAsync();

            returnedResult.ShouldBeFailureWithError(ErrorMessage);
        }

        [Fact]
        public async Task Success()
        {
            var resultInput = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());

            var returnedResult = await resultInput.ToNonGenericAsync();

            returnedResult.ShouldBeSuccess();
        }
    }
}