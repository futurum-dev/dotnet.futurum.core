using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultExtensionsToResultAsyncTests
{
    private const string ERROR_MESSAGE = "Error Message";

    public class non_generic
    {
        [Fact]
        public async Task success()
        {
            var result = await Core.Result.Result.Ok().ToResultAsync();

            result.ShouldBeSuccess();
        }

        [Fact]
        public async Task failure()
        {
            var result = await Core.Result.Result.Fail(ERROR_MESSAGE).ToResultAsync();

            result.ShouldBeFailureWithError(ERROR_MESSAGE);
        }
    }

    public class generic
    {
        [Fact]
        public async Task success()
        {
            var value = Guid.NewGuid();
            var resultValue = value.ToResultOk();

            var result = await resultValue.ToResultAsync();

            result.ShouldBeSuccessWithValue(value);
        }

        [Fact]
        public async Task failure()
        {
            var result = await Core.Result.Result.Fail<Guid>(ERROR_MESSAGE).ToResultAsync();

            result.ShouldBeFailureWithError(ERROR_MESSAGE);
        }
    }
}